using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Collini.GestioneInterventi.Dal;

public class ColliniDbContextFactory : IDesignTimeDbContextFactory<ColliniDbContext>
{
    public ColliniDbContext CreateDbContext(string[] args)
    {
        var options = CreateDbContextOptions(GetConnectionString());

        return new ColliniDbContext(options, null);
    }

    public static DbContextOptions<ColliniDbContext> CreateDbContextOptions(string connectionString)
    {
        var builder = new DbContextOptionsBuilder<ColliniDbContext>();

        builder.UseSqlServer(connectionString, e => {
            e.CommandTimeout(3600);
            e.EnableRetryOnFailure();
        });

        return builder.Options;
    }

    private static string GetConnectionString()
    {
        var basePath = AppContext.BaseDirectory;
        basePath= Path.GetDirectoryName(basePath);
        basePath = Path.GetDirectoryName(basePath);
        basePath = Path.GetDirectoryName(basePath);
        basePath = Path.GetDirectoryName(basePath);
        basePath = Path.GetDirectoryName(basePath);
        basePath = Path.Combine(basePath, "Collini.GestioneInterventi.WebApi");

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json");
        var config = builder.Build();

        return config.GetConnectionString("Default");
    }
}