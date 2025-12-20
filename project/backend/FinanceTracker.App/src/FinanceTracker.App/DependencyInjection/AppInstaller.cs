using Serilog;

namespace FinanceTracker.App.DependencyInjection;

public static class AppInstaller
{
    public static IHostBuilder AddLogging(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((context, loggerConfig) =>
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName());
    }
}
