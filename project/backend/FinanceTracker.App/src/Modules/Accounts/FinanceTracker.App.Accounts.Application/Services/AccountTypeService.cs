using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.Accounts.Application.Contracts.Repositories;
using FinanceTracker.App.Accounts.Application.Contracts.Services;
using FinanceTracker.App.Accounts.Application.Contracts.UnitOfWork;
using FinanceTracker.App.ShareKernel.Application.Errors;
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
        var accountTypeDto = accountType?.ToDto(languageContext.CurrentLanguageCode);
        return accountTypeDto is null
            ? AppError.NotFound($"Account Type with id: {id} was not found")
            : Result.Ok();
    }

    public async Task<Result<AccountTypeDto?>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return AppError.Validation("Account type code cannot be empty");
        }

        var accountType = await repository.GetByCodeAsync(code, languageContext.CurrentLanguageCode, cancellationToken);
        var result = accountType?.ToDto(languageContext.CurrentLanguageCode);

        return result is null
            ? AppError.NotFound($"Account type with code: {code} was not found")
            : result;
    }

    public async Task<Result<PaginationResult<AccountTypeDto>>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var result = await repository.GetPagedAsync(settings, languageContext.CurrentLanguageCode, includeArchived, cancellationToken);
        var dtoResult = new PaginationResult<AccountTypeDto>(
            result.Data.Select(at => at.ToDto(languageContext.CurrentLanguageCode)).ToList(),
            settings,
            result.TotalCount
        );
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
