using FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;
using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.Accounts.Application.Contracts.Repositories;
using FinanceTracker.App.Accounts.Application.Contracts.Services;
using FinanceTracker.App.Accounts.Application.Contracts.UnitOfWork;
using FinanceTracker.App.ShareKernel.Application.Errors;
using FinanceTracker.App.ShareKernel.Application.Localization;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.App.Accounts.Application.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
public sealed class AccountTypeService(
    ILanguageContext languageContext,
    IAccountTypeRepository repository,
    IAccountsUnitOfWorkManager unitOfWorkManager,
    ILogger<AccountTypeService> logger
) : IAccountTypeService
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<AccountTypeDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accountType = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);
        var accountTypeDto = accountType?.ToDto(languageContext.CurrentLanguageCode);
        return accountTypeDto is null
            ? AppError.NotFound($"Account Type with id: {id} was not found")
            : Result.Ok();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="code"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<AccountTypeDto?>> GetByCodeAsync(string code, CancellationToken cancellationToken = default
    )
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="settings"><inheritdoc/></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<PaginationResult<AccountTypeDto>>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var result = await repository.GetPagedAsync(settings, languageContext.CurrentLanguageCode, includeArchived,
            cancellationToken
        );
        var dtoResult = result.ToPaginationResult(settings, p => p.ToDto(languageContext.CurrentLanguageCode));
        return dtoResult;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<IReadOnlyList<AccountTypeDto>>> GetAllAsync(bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var accountTypes =
            await repository.GetAllAsync(languageContext.CurrentLanguageCode, includeArchived, cancellationToken);
        var accountTypeDtos = accountTypes.Select(x => x.ToDto(languageContext.CurrentLanguageCode)).ToList();
        return Result.Ok<IReadOnlyList<AccountTypeDto>>(accountTypeDtos);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="dto"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<AccountTypeDto>> CreateAsync(CreateAccountTypeDto dto,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(dto.Code))
            return AppError.Validation("AccountType code is required.");

        if (string.IsNullOrWhiteSpace(dto.Description))
            return AppError.Validation("Description is required.");

        if (await repository.ExistsByCodeAsync(dto.Code, cancellationToken))
            return AppError.Conflict($"Account type with code: {dto.Code} is already exists.");

        if (dto.Translations?.CheckDuplicates(out var duplicatedLanguages) ?? false)
            return AppError.Conflict($"Duplicate language codes found: {duplicatedLanguages}");

        var accountType = dto.ToModel();

        if (dto.Translations?.Any() ?? false)
        {
            foreach (var translation in dto.Translations)
            {
                accountType.Translations.Add(translation.ToModel(accountType.Id));
            }
        }

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            var created = await repository.AddAsync(accountType, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            return Result.Ok(created.ToDto(languageContext.CurrentLanguageCode));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while adding new account");
            return AppError.Unexpected($"Failed to create account type.");
        }
    }

    public Task<Result<AccountTypeDto>> UpdateAsync(UpdateAccountTypeDto dto,
        CancellationToken cancellationToken = default
    )
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
