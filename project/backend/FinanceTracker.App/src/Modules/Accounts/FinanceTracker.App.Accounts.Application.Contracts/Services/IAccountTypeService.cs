using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.ShareKernel.Application.Pagination;

namespace FinanceTracker.App.Accounts.Application.Contracts.Services;

public interface IAccountTypeService
{
    Task<AccountTypeDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<AccountTypeDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<PaginationResult<AccountTypeDto>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AccountTypeDto>> GetAllAsync(
        bool includeArchived = false,
        CancellationToken cancellationToken = default);

    Task<AccountTypeDto> CreateAsync(CreateAccountTypeDto dto, CancellationToken cancellationToken = default);

    Task<AccountTypeDto> UpdateAsync(UpdateAccountTypeDto dto, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task ArchiveAsync(Guid id, CancellationToken cancellationToken = default);

    Task UnarchiveAsync(Guid id, CancellationToken cancellationToken = default);
}
