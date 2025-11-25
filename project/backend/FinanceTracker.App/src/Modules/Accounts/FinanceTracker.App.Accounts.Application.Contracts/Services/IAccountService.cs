using FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;
using FinanceTracker.App.ShareKernel.Application.Pagination;

namespace FinanceTracker.App.Accounts.Application.Contracts.Services;

public interface IAccountService
{
    Task<AccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<AccountDto?> GetByIdForUserAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<PaginationResult<AccountDto>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AccountDto>> GetUserAccountsAsync(
        Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<AccountDto?> GetDefaultAccountForUserAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<AccountDto> CreateAsync(CreateAccountDto dto, CancellationToken cancellationToken = default);

    Task<AccountDto> UpdateAsync(UpdateAccountDto dto, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task ArchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task UnarchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task SetAsDefaultAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}
