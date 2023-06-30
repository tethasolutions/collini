using Collini.GestioneInterventi.Application.Security;
using Collini.GestioneInterventi.Application.Session;
using Microsoft.Extensions.DependencyInjection;

namespace Collini.GestioneInterventi.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication<TAccessTokenProvider>(this IServiceCollection services)
        where TAccessTokenProvider : class, IAccessTokenProvider
    {
        services
            .AddScoped<ISecurityContextFactory, SecurityContextFactory>()
            .AddScoped<ISecurityService, SecurityService>()
            .AddScoped<IAccessTokenProvider, TAccessTokenProvider>();

        return services;
    }
}