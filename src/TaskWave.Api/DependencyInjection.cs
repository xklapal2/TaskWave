namespace TaskWave.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        _ = services.AddControllers();
        _ = services.AddSwaggerGen();
        _ = services.AddProblemDetails();

        return services;
    }
}