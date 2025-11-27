using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FluentResults;

namespace FinanceTracker.App.Accounts.Application.Contracts.Services;

public interface IAccountTypeService
{
    Task<Result<AccountTypeDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result<AccountTypeDto?>> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<Result<PaginationResult<AccountTypeDto>>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<AccountTypeDto>>> GetAllAsync(
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<Result<AccountTypeDto>> CreateAsync(CreateAccountTypeDto dto, CancellationToken cancellationToken = default);

    Task<Result<AccountTypeDto>> UpdateAsync(UpdateAccountTypeDto dto, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result> ArchiveAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result> UnarchiveAsync(Guid id, CancellationToken cancellationToken = default);
}
