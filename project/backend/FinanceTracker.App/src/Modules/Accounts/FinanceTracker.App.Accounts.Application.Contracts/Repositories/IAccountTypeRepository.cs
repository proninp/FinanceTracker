using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Application.Pagination;

namespace FinanceTracker.App.Accounts.Application.Contracts.Repositories;

public interface IAccountTypeRepository
{
    Task<AccountType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<AccountType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<PaginationResult<AccountType>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AccountType>> GetAllAsync(
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<AccountType> AddAsync(AccountType accountType, CancellationToken cancellationToken = default);

    Task UpdateAsync(AccountType accountType, CancellationToken cancellationToken = default);

    Task DeleteAsync(AccountType accountType, CancellationToken cancellationToken = default);
}
