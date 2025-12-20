using FinanceTracker.App.Accounts.Application.Contracts.Repositories;
using FinanceTracker.App.Accounts.Application.Contracts.UnitOfWork;
using FinanceTracker.App.Infrastructure.Repositories.Repositories;
using FinanceTracker.App.Infrastructure.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.App.Infrastructure.Repositories;

/// <summary>
/// Класс для регистрации репозиториев в DI контейнере.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет репозитории модуля Accounts в контейнер зависимостей.
    /// </summary>
    public static IServiceCollection AddAccountsRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
        services.AddScoped<IAccountsUnitOfWorkManager, AccountsUnitOfWorkManager>();

        return services;
    }
}
