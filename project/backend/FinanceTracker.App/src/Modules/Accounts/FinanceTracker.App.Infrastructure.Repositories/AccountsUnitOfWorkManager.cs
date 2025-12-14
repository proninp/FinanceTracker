using FinanceTracker.App.Accounts.Application.Contracts.UnitOfWork;
using FinanceTracker.App.Infrastructure.EntityFramework;
using FinanceTracker.App.SharedKernel.Infrastructure.UnitOfWork;

namespace FinanceTracker.App.Infrastructure.Repositories;

public sealed class AccountsUnitOfWorkManager(AccountsDbContext context)
    : UnitOfWorkManager<AccountsDbContext>(context), IAccountsUnitOfWorkManager
{
}
