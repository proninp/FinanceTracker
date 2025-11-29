using FinanceTracker.App.Infrastructure.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FinanceTracker.App.Infrastructure.EntityFramework;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isUseSensitiveLogging = false
    )
    {
        services.Configure<AccountsDbSettings>(configuration.GetSection(nameof(AccountsDbSettings)));
        services.AddDbContext<AccountsDbContext>((provider, options) =>
            {
                var dbSettings = provider.GetRequiredService<IOptions<AccountsDbSettings>>().Value;
                options.UseNpgsql(dbSettings.GetConnectionString());
                if (isUseSensitiveLogging)
                {
                    options.EnableSensitiveDataLogging();
                }
            },
            ServiceLifetime.Scoped,
            ServiceLifetime.Singleton
        );
        return services;
    }

    public static async Task EnsureMigrationsApplied(this IServiceProvider provider)
    {
        await using var scope = provider.CreateAsyncScope();
        var accountsContext = scope.ServiceProvider.GetRequiredService<AccountsDbContext>();
        var pendingMigrations = await accountsContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
            throw new Exception($"Database is not fully migrated for {nameof(AccountsDbContext)}.");
    }
}
