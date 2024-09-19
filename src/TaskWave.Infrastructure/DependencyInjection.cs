using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TaskWave.Application.Common.Interfaces;
using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Infrastructure.Persistence;
using TaskWave.Infrastructure.Persistence.Repositories;
using TaskWave.Infrastructure.Security;
using TaskWave.Infrastructure.Security.CurrentUserProvider;
using TaskWave.Infrastructure.Security.PolicyEnforcer;
using TaskWave.Infrastructure.Security.TokenGenerator;
using TaskWave.Infrastructure.Security.TokenValidation;

namespace TaskWave.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddHttpContextAccessor()
                    .AddAuthentication(configuration)
                    .AddAuthorization()
                    .AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        _ = services.AddDatabase<AppDbContext>(configuration);

        // repositories
        _ = services.AddScoped<IUserRepository, UserRepository>();
        _ = services.AddScoped<IGroupRepository, GroupRepository>();

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

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();

        return services;
    }
}