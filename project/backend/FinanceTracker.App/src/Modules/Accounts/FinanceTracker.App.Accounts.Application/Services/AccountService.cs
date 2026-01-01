using FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;
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

internal class AccountService(
    ILanguageContext languageContext,
    IAccountRepository repository,
    IAccountsUnitOfWorkManager unitOfWorkManager,
    ILogger<AccountService> logger
) : IAccountService
{
    private const string AccountNotFound = "Account with id: {0} was not found";
    private const string AccountNotFoundForUser = "Account with id: {0} was not found for user {1}";
    private const string AccountNameIsRequired = "Account name is required.";
    private const string DuplicateLanguagesFound = "Duplicate language codes for account {0} found: {1}";

    // Create account messages
    private const string AccountAddedSuccessLog = "Account {AccountName} has been added for user {UserId} successfully";

    private const string ErrorAddingAccountLog =
        "An error occured while adding an account {AccountName} for user {UserId}";

    private const string ErrorAddingAccount = "An error occured while adding an account";

    // Update account messages
    private const string AccountUpdatedSuccessLog =
        "Account {AccountName} with {AccountId} id has been updated successfully";

    private const string ErrorUpdatingAccountLog =
        "An error occured while updating an account {AccountId} for user {UserId}";

    private const string ErrorUpdatingAccount = "An error occured while updating an account";

    // Delete account messages
    private const string AccountAlreadyDeletedLog = "Account with {AccountId} id has already been deleted";

    private const string AccountDeletedSuccessLog =
        "Account {AccountName} with {AccountId} id has been deleted successfully";

    private const string ErrorDeletingAccountLog =
        "An error occured while deleting an account {AccountId}";

    private const string ErrorDeletingAccount = "An error occured while deleting an account";

    private const string ErrorDeletingDefaultAccountLog =
        "Сannot delete the default account {AccountId}";

    private const string ErrorDeletingDefaultAccount =
        "You cannot delete the default account {0}, make sure that you have set the new account as default";

    // Archive account messages
    private const string AccountAlreadyArchived = "The account {0} is already archived";

    private const string AccountAlreadyUnarchived = "The account {0} is already unarchived";

    private const string AccountArchivedSuccessLog =
        "Account {AccountName} with {AccountId} id has been archived successfully";

    private const string ErrorArchivingAccountLog =
        "An error occured while archiving an account {AccountId}";

    private const string ErrorArchivingAccount =
        "An error occured while archiving an account";

    private const string AccountUnarchivedSuccessLog =
        "Account {AccountName} with {AccountId} id has been unarchived successfully";

    private const string ErrorUnarchivingAccountLog =
        "An error occured while unarchiving an account {AccountId}";

    private const string ErrorUnarchivingAccount =
        "An error occured while unarchiving an account";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<Result<AccountDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accountResult = await GetAccountResultAsync(id, cancellationToken);
        if (accountResult.IsFailed)
            return Result.Fail(accountResult.Errors);

        var accountTypeDto = accountResult.Value.ToDto(languageContext.CurrentLanguageCode);
        return Result.Ok(accountTypeDto);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<Result<AccountDto>> GetByIdForUserAsync(Guid id, Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        var account =
            await repository.GetByIdForUserAsync(id, userId, languageContext.CurrentLanguageCode, cancellationToken);
        if (account is null)
            return AppError.NotFound(string.Format(AccountNotFoundForUser, id, userId));

        var accountDto = account.ToDto(languageContext.CurrentLanguageCode);
        return Result.Ok(accountDto);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<Result<PaginationResult<AccountDto>>> GetPagedAsync(PaginationSettings settings, Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var userAccountsResult = await repository.GetPagedAsync(
            settings,
            userId,
            languageContext.CurrentLanguageCode,
            includeArchived,
            cancellationToken
        );

        var dtoResult =
            userAccountsResult.ToPaginationResult(settings, a => a.ToDto(languageContext.CurrentLanguageCode));
        return Result.Ok(dtoResult);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<Result<IReadOnlyList<AccountDto>>> GetUserAccountsAsync(Guid userId, bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var accounts = await repository.GetUserAccountsAsync(
            userId,
            languageContext.CurrentLanguageCode,
            includeArchived,
            cancellationToken
        );

        var accountDtos = accounts.ToDto(languageContext.CurrentLanguageCode);
        return Result.Ok<IReadOnlyList<AccountDto>>(accountDtos);
    }

    public async Task<Result<AccountDto>> GetDefaultAccountForUserAsync(Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        var account =
            await repository.GetDefaultAccountForUserAsync(userId, languageContext.CurrentLanguageCode,
                cancellationToken
            );
        return account is null
            ? AppError.NotFound($"The default account for user with id: {userId} was not found")
            : Result.Ok(account.ToDto(languageContext.CurrentLanguageCode));
    }

    public async Task<Result<AccountDto>> CreateAsync(CreateAccountDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var account = dto.ToModel();
        if (string.IsNullOrEmpty(account.Name))
            return AppError.Validation(AccountNameIsRequired);

        if (dto.Translations?.CheckDuplicates(out var duplicateLanguages) ?? false)
            return AppError.Validation(string.Format(DuplicateLanguagesFound, dto.Name, duplicateLanguages));

        AddTranslations(account, dto.Translations);

        try
        {
            unitOfWorkManager.StartUnitOfWork();

            if (account.IsDefault)
                await ClearPreviousDefaultAccount(account.UserId, cancellationToken);

            await repository.AddAsync(account, cancellationToken);

            await unitOfWorkManager.SaveChangesAsync(cancellationToken);

            logger.LogInformation(AccountAddedSuccessLog, dto.Name, account.UserId);
            return Result.Ok(account.ToDto(languageContext.CurrentLanguageCode));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorAddingAccountLog, dto.Name, account.UserId);
            return AppError.Unexpected(ErrorAddingAccount);
        }
    }

    public async Task<Result<AccountDto>> UpdateAsync(UpdateAccountDto dto,
        CancellationToken cancellationToken = default
    )
    {
        var account = await repository.GetByIdAsync(dto.Id, languageContext.CurrentLanguageCode, cancellationToken);
        if (account is null)
            return AppError.NotFound(string.Format(AccountNotFound, dto.Id));

        var name = dto.Name.Trim();
        if (string.IsNullOrEmpty(name))
            return AppError.Validation(AccountNameIsRequired);

        if (dto.Translations?.CheckDuplicates(out var duplicateLanguages) ?? false)
            return AppError.Validation(string.Format(DuplicateLanguagesFound, dto.Name, duplicateLanguages));

        try
        {
            unitOfWorkManager.StartUnitOfWork();

            account.BankId = dto.BankId;
            account.Name = name;
            account.CreditLimit = dto.CreditLimit;
            account.IsIncludeInBalance = dto.IsIncludeInBalance;
            account.IsDefault = dto.IsDefault;
            if (account.IsDefault)
                await ClearPreviousDefaultAccount(account.UserId, cancellationToken);

            account.Translations.Clear();
            AddTranslations(account, dto.Translations);

            await repository.UpdateAsync(account, cancellationToken);

            await unitOfWorkManager.SaveChangesAsync(cancellationToken);

            logger.LogInformation(AccountUpdatedSuccessLog, dto.Name, account.Id);
            return Result.Ok(account.ToDto(languageContext.CurrentLanguageCode));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorUpdatingAccountLog, dto.Id, account.UserId);
            return AppError.Unexpected(ErrorUpdatingAccount);
        }
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);
        if (account is null)
        {
            logger.LogInformation(AccountAlreadyDeletedLog, id);
            return Result.Ok();
        }

        if (account.IsDefault)
        {
            logger.LogError(ErrorDeletingDefaultAccountLog, id);
            return AppError.Forbidden(string.Format(ErrorDeletingDefaultAccount, id));
        }

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await repository.DeleteAsync(account, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            logger.LogInformation(AccountDeletedSuccessLog, account.Name, id);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorDeletingAccountLog, id);
            return AppError.Unexpected(ErrorDeletingAccount);
        }
    }

    public async Task<Result> ArchiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);
        if (account is null)
            return AppError.NotFound(string.Format(AccountNotFound, id));

        if (account.IsArchived)
            return Result.Ok().WithSuccess(string.Format(AccountAlreadyArchived, id));

        account.IsArchived = true;

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await repository.UpdateAsync(account, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            logger.LogInformation(AccountArchivedSuccessLog, account.Name, id);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorArchivingAccountLog, id);
            return AppError.Unexpected(ErrorArchivingAccount);
        }
    }

    public async Task<Result> UnarchiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);
        if (account is null)
            return AppError.NotFound(string.Format(AccountNotFound, id));

        if (!account.IsArchived)
            return Result.Ok().WithSuccess(string.Format(AccountAlreadyUnarchived, id));

        account.IsArchived = false;

        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await repository.UpdateAsync(account, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            logger.LogInformation(AccountUnarchivedSuccessLog, account.Name, id);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ErrorUnarchivingAccountLog, id);
            return AppError.Unexpected(ErrorUnarchivingAccount);
        }
    }

    private async Task<Result<Account>> GetAccountResultAsync(Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var account = await repository.GetByIdAsync(id, languageContext.CurrentLanguageCode, cancellationToken);
        return account is null
            ? AppError.NotFound(string.Format(AccountNotFound, id)) // TODO: Add #Localization for error messages
            : Result.Ok(account);
    }

    private void AddTranslations(Account account, ICollection<AccountTranslationDto>? translationDtos)
    {
        if (translationDtos is not null && translationDtos.Count != 0)
        {
            foreach (var translation in translationDtos)
            {
                account.Translations.Add(translation.ToModel(account.Id));
            }
        }
    }

    private async Task ClearPreviousDefaultAccount(Guid userId, CancellationToken cancellationToken)
    {
        var accountToChangeDefault = await repository.GetDefaultAccountForUserAsync(userId,
            languageContext.CurrentLanguageCode, cancellationToken
        );

        if (accountToChangeDefault != null)
        {
            accountToChangeDefault.IsDefault = false;
            await repository.UpdateAsync(accountToChangeDefault, cancellationToken);
        }
    }
}
