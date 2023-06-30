using AutoMapper;
using Collini.GestioneInterventi.Application;
using Collini.GestioneInterventi.Framework.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Collini.GestioneInterventi.Application.Session;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Framework;
using Collini.GestioneInterventi.Framework.Configuration;
using Collini.GestioneInterventi.WebApi.Auth;
using Collini.GestioneInterventi.WebApi.Configuration;
using Collini.GestioneInterventi.WebApi.ModelBinders;
using Newtonsoft.Json;

namespace Collini.GestioneInterventi.WebApi;

public static class WebApiConfiguration
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        // MVC
        services
            .AddControllers(e =>
            {
                e.Filters.Add<AuthorizeFilter>();
                e.ModelBinderProviders.Insert(0, new DateTimeOffsetModelBinderProvider());
            })
            .ConfigureApiBehaviorOptions(e => e.SuppressModelStateInvalidFilter = true)
            .AddControllersAsServices()
            .AddNewtonsoftJson(e => SetupJsonSettings(e.SerializerSettings));

        // Get configuration from appsettings.json
        var colliniConfiguration = PerformBaseSetup(services, configuration);

        // App
        services
            .AddHttpContextAccessor()
            .AddFramework<ColliniSession>(colliniConfiguration)
            .AddDal()
            .AddApplication<AccessTokenProvider>()
            .AddMappings();

        return services;
    }

    private static void SetupJsonSettings(JsonSerializerSettings settings)
    {
        settings.DateParseHandling = DateParseHandling.DateTimeOffset;
        settings.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
    }

    private static void AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApplicationConfiguration).Assembly);

        using (var serviceProvider = services.BuildServiceProvider())
        {
            serviceProvider
                .GetRequiredService<IMapper>().ConfigurationProvider
                .AssertConfigurationIsValid();
        }
    }

    private static IColliniConfiguration PerformBaseSetup(IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection("Collini");
        var colliniConfiguration = section.Get<ColliniConfiguration>();

        if (colliniConfiguration == null)
        {
            throw new ColliniException("Configuration section 'Collini' not found.");
        }

        if (colliniConfiguration is { AllowCors: true })
        {
            AddCors(services, colliniConfiguration);
        }

        return colliniConfiguration;
    }

    private static void AddCors(IServiceCollection services, IColliniConfiguration configuration)
    {
        var origins = configuration.CorsOrigins?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      ?? Array.Empty<string>();

        services.AddCors(e =>
            e.AddPolicy("Collini",
                p => p.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(origins)
            )
        );
    }


    public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app
                .UseDeveloperExceptionPage();
        }
        else
        {
            app
                .UseHsts()
                .UseHttpsRedirection();
        }

        app
            .UseDefaultFiles()
            .UseStaticFiles()
            .UseRouting()
            .UseCors("Collini")
            .UseExceptionHandler(appError => appError.Run(HandleError))
            .UseAuthorization()
            .UseAuthentication()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        return app;
    }

    private static async Task HandleError(HttpContext context)
    {
        var response = context.Response;
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var error = feature?.Error is AggregateException
            ? feature.Error.InnerException
            : feature?.Error;
        var configuration = context.RequestServices.GetRequiredService<IColliniConfiguration>();

        if (configuration.AllowCors)
        {
            response.Headers.Add("access-control-allow-credentials", "true");
            response.Headers.Add("access-control-allow-headers", "authorization,content-type");
            response.Headers.Add("access-control-allow-origin", configuration.CorsOrigins);
        }

        switch (error)
        {
            case UnauthorizedException _:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case NotFoundException notFoundException:
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                await response.WriteAsync(notFoundException.Message);
                break;
            case ColliniException colliniException:
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                await response.WriteAsync(colliniException.GetMessageRecursive());
                break;
            default:
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(error?.Message ?? "An error occurred.");
                break;
        }
    }
}