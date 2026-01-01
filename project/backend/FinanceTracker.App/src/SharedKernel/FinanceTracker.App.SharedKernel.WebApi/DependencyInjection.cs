using FinanceTracker.App.SharedKernel.Infrastructure;
using FinanceTracker.App.SharedKernel.WebApi.Localization;
using FinanceTracker.App.ShareKernel.Application.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.App.SharedKernel.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedKernel(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ILanguageContext, HttpLanguageContext>();

        services.AddInfrastructure();

        return services;
    }
}
