using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Collini.GestioneInterventi.Dal;

public static class DalConfiguration
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services
            .AddSingleton(DbContextOptionsFactory)
            .AddScoped<IColliniDbContext, ColliniDbContext>()
            .AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    private static DbContextOptions DbContextOptionsFactory(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Default");

        return ColliniDbContextFactory.CreateDbContextOptions(connectionString);
    }
}