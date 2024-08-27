using Microsoft.Extensions.DependencyInjection;

namespace TaskWave.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
        );

        return services;
    }
}