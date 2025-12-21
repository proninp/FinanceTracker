using FinanceTracker.App.Accounts.Application.Contracts.UnitOfWork;
using FinanceTracker.App.SharedKernel.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.App.Infrastructure.Repositories.UnitOfWork;

public sealed class AccountsTransactionExecutor(IAccountsUnitOfWorkManager unitOfWorkManager, ILogger logger)
    : TransactionExecutor(unitOfWorkManager, logger), IAccountsTransactionExecutor
{
}
