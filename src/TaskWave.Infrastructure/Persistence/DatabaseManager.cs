using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskWave.Infrastructure.Persistence;

public static class DatabaseManager
{
    public static string DatabaseConnectionStringKey => "DB";

    /// <summary>
    /// Creates a database context service.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">If null, an InMemory database will be created</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>

    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, ConfigurationManager configuration) where T : DbContext
    {
        if (configuration == null || configuration.GetConnectionString(DatabaseConnectionStringKey) is not string cnnString)
        {
            throw new ArgumentException($"[{nameof(DatabaseManager)}.{nameof(AddDatabase)}]: Connection string for the database is missing or invalid.");
        }

        _ = services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                cnnString,
                ServerVersion.AutoDetect(cnnString)
            )
        );

        return services;
    }

    public static void EnsureCreated<T>(IServiceProvider services) where T : DbContext
    {
        using IServiceScope scope = services.CreateScope();
        T db = scope.ServiceProvider.GetRequiredService<T>();
        _ = db.Database.EnsureDeleted();
        _ = db.Database.EnsureCreated();
    }

    public static void Migrate<T>(IServiceProvider services) where T : DbContext
    {
        using IServiceScope scope = services.CreateScope();
        T db = scope.ServiceProvider.GetRequiredService<T>();
        db.Database.Migrate();
    }
}