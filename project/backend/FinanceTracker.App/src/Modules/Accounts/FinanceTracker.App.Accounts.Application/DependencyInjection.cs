using FinanceTracker.App.Accounts.Application.Contracts.Services;
using FinanceTracker.App.Accounts.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.App.Accounts.Application;

/// <summary>
/// Методы расширения для регистрации сервисов приложения
/// в контейнере зависимостей.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует сервисы прикладного уровня.
    /// </summary>
    /// <param name="services">Коллекция сервисов DI-контейнера.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountTypeService, AccountTypeService>();
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}
