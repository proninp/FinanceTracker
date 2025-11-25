using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Application.Pagination;

namespace FinanceTracker.App.Accounts.Application.Contracts.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Account?> GetByIdForUserAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<PaginationResult<Account>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Account>> GetUserAccountsAsync(
        Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<Account?> GetDefaultAccountForUserAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsUserAccountOwnerAsync(Guid accountId, Guid userId, CancellationToken cancellationToken = default);

    Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default);

    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);

    Task DeleteAsync(Account account, CancellationToken cancellationToken = default);

    Task<int> CountUserAccountsAsync(Guid userId, bool includeArchived = false, CancellationToken cancellationToken = default);
}
