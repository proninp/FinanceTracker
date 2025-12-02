using FinanceTracker.App.SharedKernel.Infrastructure.UnitOfWork;
using FinanceTracker.App.ShareKernel.Application.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.App.SharedKernel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWorkManager<>), typeof(UnitOfWorkManager<>));
        return services;
    }
}
