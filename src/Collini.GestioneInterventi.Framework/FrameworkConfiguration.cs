using Collini.GestioneInterventi.Framework.Common;
using Collini.GestioneInterventi.Framework.Configuration;
using Collini.GestioneInterventi.Framework.IO;
using Collini.GestioneInterventi.Framework.Security;
using Collini.GestioneInterventi.Framework.Session;
using Microsoft.Extensions.DependencyInjection;

namespace Collini.GestioneInterventi.Framework;

public static class FrameworkConfiguration
{
    public static IServiceCollection AddFramework<TSession>(this IServiceCollection services, IColliniConfiguration configuration)
        where TSession : class, IColliniSession
    {
        services
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>()
            .AddSingleton<IPasswordGenerator, PasswordGenerator>()
            .AddScoped<IColliniSession, TSession>()
            .AddSingleton<IGuidGenerator, GuidGenerator>()
            .AddSingleton(configuration)
            .AddSingleton<IMimeTypeProvider, MimeTypeProvider>();

        return services;
    }
}