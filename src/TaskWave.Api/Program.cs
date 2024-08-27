using Serilog;

using TaskWave.Api;
using TaskWave.Api.Common;
using TaskWave.Api.Common.Http.RouteConstraints;
using TaskWave.Infrastructure;
using TaskWave.Application;

using TaskWave.Infrastructure.Persistence;

{
    SetupLogProvider.CreateBootstrapLogger();
}

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

{
    builder.SetupLogger();

    _ = builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddPresentation();
}

{
    try
    {
        WebApplication app = builder.Build();

        app.Services.MigrateDatabase();

        RouteOptions routeOptions = new();
        routeOptions.ConstraintMap.Add("ulid", typeof(UlidRouteConstraint));

        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
        }

        _ = app.UseHttpsRedirection();
        _ = app.MapControllers();

        app.Run();
    }
    catch (HostAbortedException)
    {
        throw;
    }
    catch (Exception e)
    {
        Log.Fatal(e, "The application encountered a fatal error during startup and will now terminate.");
    }
    finally
    {
        Log.CloseAndFlush();
    }
}