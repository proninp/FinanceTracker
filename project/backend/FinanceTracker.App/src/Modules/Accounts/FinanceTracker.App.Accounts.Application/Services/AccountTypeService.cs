using FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;
using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.Accounts.Application.Contracts.Repositories;
using FinanceTracker.App.Accounts.Application.Contracts.Services;
using FinanceTracker.App.Accounts.Application.Contracts.UnitOfWork;
using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Application.Errors;
using FinanceTracker.App.ShareKernel.Application.Localization;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.App.Accounts.Application.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
internal sealed class AccountTypeService(
    ILanguageContext languageContext,
    IAccountTypeRepository repository,
    IAccountsUnitOfWorkManager unitOfWorkManager,
    ILogger<AccountTypeService> logger
) : IAccountTypeService
{
    private const string AccountTypeNotFound = "Account Type with id: {0} was not found";
    private const string AccountTypeCodeAlreadyExists = "Account type with code: {0} is already exists.";
    private const string AccountTypeCodeNotFound = "Account type with code: {0} was not found.";
    private const string CodeIsRequired = "Account Type code is required.";
    private const string DescriptionIsRequired = "Account Type Description is required.";
    private const string DuplicateLanguagesFound = "Duplicate language codes for account {0} found: {1}";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<AccountTypeDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accountTypeResult = await GetAccountTypeResultAsync(id, cancellationToken);
        if (accountTypeResult.IsFailed)
            return Result.Fail(accountTypeResult.Errors);

        var accountTypeDto = accountTypeResult.Value.ToDto(languageContext.CurrentLanguageCode);
        return Result.Ok(accountTypeDto);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="code"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<AccountTypeDto>> GetByCodeAsync(string code, CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return AppError.Validation(CodeIsRequired);
        }

        var accountType = await repository.GetByCodeAsync(code, languageContext.CurrentLanguageCode, cancellationToken);
        if (accountType is null)
            return AppError.NotFound(string.Format(AccountTypeCodeNotFound, code));

        var accountTypeDto = accountType.ToDto(languageContext.CurrentLanguageCode);

        return Result.Ok(accountTypeDto);
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
        var code = dto.Code.Trim();
        var description = dto.Description.Trim();

        var validationResult = ValidateRequiredFields(code, description);
        if (validationResult.IsFailed)
            return validationResult;

        if (await repository.ExistsByCodeAsync(code, cancellationToken))
            return AppError.Conflict(string.Format(AccountTypeCodeAlreadyExists, dto.Code));

        if (dto.Translations?.CheckDuplicates(out var duplicatedLanguages) ?? false)
            return AppError.Conflict(string.Format(DuplicateLanguagesFound, dto.Code, duplicatedLanguages));

        var accountType = dto.ToModel();

        AddTranslations(accountType, dto.Translations);

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            var created = await repository.AddAsync(accountType, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Account type {AccountTypeCode} created successfully", created.Code);
            return Result.Ok(created.ToDto(languageContext.CurrentLanguageCode));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while adding a new account type with code {AccountTypeCode}", code);
            // TODO: Add #Localization for error messages
            return AppError.Unexpected("Failed to create a new account type.");
        }
    }

    public async Task<Result<AccountTypeDto>> UpdateAsync(UpdateAccountTypeDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = ValidateRequiredFields(dto.Code, dto.Description);
        if (validationResult.IsFailed)
            return validationResult;

        var accountTypeResult = await GetAccountTypeResultAsync(dto.Id, cancellationToken);
        if (accountTypeResult.IsFailed)
            return Result.Fail(accountTypeResult.Errors);
        var accountType = accountTypeResult.Value;

        if (accountType.Code != dto.Code &&
            await repository.ExistsByCodeAsync(dto.Code, cancellationToken))
        {
            return AppError.Conflict(string.Format(AccountTypeCodeAlreadyExists, dto.Code));
        }

        if (dto.Translations?.CheckDuplicates(out var duplicatedLanguages) ?? false)
            return AppError.Conflict(string.Format(DuplicateLanguagesFound, dto.Code, duplicatedLanguages));

        accountType.Code = dto.Code.Trim();
        accountType.Description = dto.Description.Trim();
        accountType.IsArchived = dto.IsArchived;

        accountType.Translations.Clear();

        AddTranslations(accountType, dto.Translations);

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await repository.UpdateAsync(accountType, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Account type {AccountTypeId} updated successfully", dto.Id);
            return Result.Ok(accountType.ToDto(languageContext.CurrentLanguageCode));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while updating an account type {AccountTypeId}", dto.Id);
            // TODO: Add #Localization for error messages
            return AppError.Unexpected($"Failed to update the account type {dto.Id}");
        }
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accountType = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);
        if (accountType is null)
            return Result.Ok();

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await repository.DeleteAsync(accountType, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Account type {AccountTypeId} deleted successfully", id);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while deleting an account type {AccountTypeId}", id);
            // TODO: Add #Localization for error messages
            return AppError.Unexpected($"Failed to delete the account type {id}");
        }
    }

    public async Task<Result> ArchiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accountTypeResult = await GetAccountTypeResultAsync(id, cancellationToken);
        if (accountTypeResult.IsFailed)
            return Result.Fail(accountTypeResult.Errors);
        var accountType = accountTypeResult.Value;

        if (accountType.IsArchived)
            return Result.Ok().WithSuccess("The account type is already archived");

        accountType.IsArchived = true;

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await repository.UpdateAsync(accountType, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while archiving an account type {AccountTypeId}", id);
            // TODO: Add #Localization for error messages
            return AppError.Unexpected("An error occured while archiving an account type");
        }
    }

    public async Task<Result> UnarchiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accountTypeResult = await GetAccountTypeResultAsync(id, cancellationToken);
        if (accountTypeResult.IsFailed)
            return Result.Fail(accountTypeResult.Errors);
        var accountType = accountTypeResult.Value;

        if (!accountType.IsArchived)
            return Result.Ok().WithSuccess("The account type is not archived");

        accountType.IsArchived = false;

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await repository.UpdateAsync(accountType, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while unarchiving the account type {AccountTypeId}", id);
            // TODO: Add #Localization for error messages
            return AppError.Unexpected("Failed to unarchive the account type");
        }
    }

    private Result ValidateRequiredFields(string code, string description)
    {
        if (string.IsNullOrWhiteSpace(code))
            return AppError.Validation(CodeIsRequired);

        if (string.IsNullOrWhiteSpace(description))
            return AppError.Validation(DescriptionIsRequired);

        return Result.Ok();
    }

    private void AddTranslations(AccountType accountType, ICollection<AccountTypeTranslationDto>? translationDtos)
    {
        if (translationDtos is not null && translationDtos.Count != 0)
        {
            foreach (var translation in translationDtos)
            {
                accountType.Translations.Add(translation.ToModel(accountType.Id));
            }
        }
    }

    private async Task<Result<AccountType>> GetAccountTypeResultAsync(Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var accountType = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);
        return accountType is null
            ? AppError.NotFound(string.Format(AccountTypeNotFound, id)) // TODO: Add #Localization for error messages
            : Result.Ok(accountType);
    }
}
