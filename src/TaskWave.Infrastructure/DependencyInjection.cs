using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TaskWave.Application.Common.Interfaces;

using TaskWave.Infrastructure.Persistence;
using TaskWave.Infrastructure.Persistence.Repositories;

using TaskWave.Infrastructure.Security.TokenGenerator;
using TaskWave.Infrastructure.Security.TokenValidation;

namespace TaskWave.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddAuthentication(configuration)
                    .AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddDatabase<AppDbContext>(configuration);

        // repositories
        _ = services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services
            .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}