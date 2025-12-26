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
    private const string UserNotAuthorized = "User {0} is not authorized to access account {1}";

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

        Account? previousDefaultAccount = null;
        if (dto.IsDefault)
        {
            previousDefaultAccount = await repository.GetDefaultAccountForUserAsync(dto.UserId,
                languageContext.CurrentLanguageCode, cancellationToken);
        }

        unitOfWorkManager.StartUnitOfWork();

        try
        {
            if (previousDefaultAccount != null)
            {
                previousDefaultAccount.IsDefault = false;
                await repository.UpdateAsync(previousDefaultAccount, cancellationToken);
            }

            await repository.AddAsync(account, cancellationToken);
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Account {AccountName} has been added for user {UserId} successfully", dto.Name,
                account.UserId
            );
            return Result.Ok(account.ToDto(languageContext.CurrentLanguageCode));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while adding an account {AccountName} for user {UserId}", dto.Name,
                account.UserId
            );
            return AppError.Unexpected("An error occured while adding an account");
        }
    }

    public Task<Result<AccountDto>> UpdateAsync(UpdateAccountDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ArchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UnarchiveAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SetAsDefaultAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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
}
