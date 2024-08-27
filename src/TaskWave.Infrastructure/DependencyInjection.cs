using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TaskWave.Application.Common.Interfaces;

using TaskWave.Infrastructure.Persistence;
using TaskWave.Infrastructure.Persistence.Repositories;

namespace TaskWave.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddDatabase<AppDbContext>(configuration);

        // repositories
        _ = services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}