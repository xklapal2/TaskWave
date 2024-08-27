using Serilog;
using Serilog.Events;

namespace TaskWave.Api.Common;

public static class SetupLogProvider
{
    public static void CreateBootstrapLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        Log.Information("Starting application {appName}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        Log.Information("Application is running under the user account: {user}", Environment.UserName);
        Log.Information("Application is running with ASPNETCORE_ENVIRONMENT: {appEnv}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
    }

    public static void SetupLogger(this WebApplicationBuilder builder)
    {
        _ = builder.Host.UseSerilog(
            (context, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext());
    }
}