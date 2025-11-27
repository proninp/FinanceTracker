using FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FluentResults;

namespace FinanceTracker.App.Accounts.Application.Contracts.Services;

public interface IAccountService
{
    Task<Result<AccountDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result<AccountDto?>> GetByIdForUserAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<Result<PaginationResult<AccountDto>>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<AccountDto>>> GetUserAccountsAsync(
        Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<Result<AccountDto?>> GetDefaultAccountForUserAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Result<AccountDto>> CreateAsync(CreateAccountDto dto, CancellationToken cancellationToken = default);

    Task<Result<AccountDto>> UpdateAsync(UpdateAccountDto dto, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<Result> ArchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<Result> UnarchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<Result> SetAsDefaultAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}
