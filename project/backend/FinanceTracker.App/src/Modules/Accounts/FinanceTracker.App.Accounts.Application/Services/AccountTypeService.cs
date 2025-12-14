using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.Accounts.Application.Contracts.Repositories;
using FinanceTracker.App.Accounts.Application.Contracts.Services;
using FinanceTracker.App.Accounts.Application.Contracts.UnitOfWork;
using FinanceTracker.App.ShareKernel.Application.Localization;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FluentResults;

namespace FinanceTracker.App.Accounts.Application.Services;

public sealed class AccountTypeService(
    ILanguageContext languageContext,
    IAccountTypeRepository repository,
    IAccountsUnitOfWorkManager unitOfWorkManager
) : IAccountTypeService
{
    public async Task<Result<AccountTypeDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accountType = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);

        return Result.Ok(accountType?.ToDto(languageContext.CurrentLanguageCode));
    }

    public Task<Result<AccountTypeDto?>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PaginationResult<AccountTypeDto>>> GetPagedAsync(PaginationSettings settings, bool includeArchived = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IReadOnlyList<AccountTypeDto>>> GetAllAsync(bool includeArchived = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AccountTypeDto>> CreateAsync(CreateAccountTypeDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AccountTypeDto>> UpdateAsync(UpdateAccountTypeDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ArchiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UnarchiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
