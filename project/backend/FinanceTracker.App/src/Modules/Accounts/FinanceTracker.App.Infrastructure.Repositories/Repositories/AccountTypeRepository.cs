using FinanceTracker.App.Accounts.Application.Contracts.Repositories;
using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.Infrastructure.EntityFramework;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FinanceTracker.App.ShareKernel.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.App.Infrastructure.Repositories.Repositories;

/// <summary>
/// Реализация репозитория для работы с типами счетов.
/// </summary>
internal sealed class AccountTypeRepository(
    AccountsDbContext context,
    IUnitOfWorkManager<AccountsDbContext> unitOfWorkManager
) : IAccountTypeRepository
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <param name="languageCode"></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<AccountType?> GetByIdAsync(Guid id, string? languageCode = null, CancellationToken cancellationToken = default)
    {
        var query = context.AccountTypes.AsNoTracking();
        query = IncludeTranslationsByLanguage(languageCode, query);
        return await query.FirstOrDefaultAsync(at => at.Id == id, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="code"><inheritdoc/></param>
    /// <param name="languageCode"></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<AccountType?> GetByCodeAsync(string code, string? languageCode = null, CancellationToken cancellationToken = default)
    {
        var query = context.AccountTypes.AsNoTracking();

        query = IncludeTranslationsByLanguage(languageCode, query);

        return await query.FirstOrDefaultAsync(at => at.Code == code, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="settings"><inheritdoc/></param>
    /// <param name="languageCode"></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<PaginationResult<AccountType>> GetPagedAsync(
        PaginationSettings settings,
        string? languageCode = null,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.AccountTypes.AsNoTracking();

        if (!includeArchived)
        {
            query = query.Where(at => !at.IsArchived);
        }

        query = IncludeTranslationsByLanguage(languageCode, query);

        var totalCount = await query.CountAsync(cancellationToken);

        if (totalCount == 0)
        {
            return PaginationResult<AccountType>.Empty(settings.PageNumber, settings.EffectivePageSize);
        }

        var data = await query
            .OrderBy(at => at.Code)
            .Skip(settings.Skip)
            .Take(settings.EffectivePageSize)
            .ToListAsync(cancellationToken);

        return new PaginationResult<AccountType>(data, settings, totalCount);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="languageCode"></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<IReadOnlyList<AccountType>> GetAllAsync(
        string? languageCode = null,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.AccountTypes.AsNoTracking();

        if (!includeArchived)
        {
            query = query.Where(at => !at.IsArchived);
        }

        query = IncludeTranslationsByLanguage(languageCode, query);

        return await query
            .OrderBy(at => at.Code)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.AccountTypes
            .AsNoTracking()
            .AnyAsync(at => at.Id == id, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="code"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await context.AccountTypes
            .AsNoTracking()
            .AnyAsync(at => at.Code == code, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="accountType"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<AccountType> AddAsync(AccountType accountType, CancellationToken cancellationToken = default)
    {
        await context.AccountTypes.AddAsync(accountType, cancellationToken);
        if (!unitOfWorkManager.IsUnitOfWorkStarted)
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
        return accountType;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="accountType"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    public async Task UpdateAsync(AccountType accountType, CancellationToken cancellationToken = default)
    {
        context.AccountTypes.Update(accountType);
        if (!unitOfWorkManager.IsUnitOfWorkStarted)
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="accountType"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    public async Task DeleteAsync(AccountType accountType, CancellationToken cancellationToken = default)
    {
        context.AccountTypes.Remove(accountType);
        if (!unitOfWorkManager.IsUnitOfWorkStarted)
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="languageCode"></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<IReadOnlyList<AccountType>> GetAllIncludingDeletedAsync(
        string? languageCode = null,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.AccountTypes
            .IgnoreQueryFilters()
            .AsNoTracking();

        if (!includeArchived)
        {
            query = query.Where(at => !at.IsArchived);
        }

        query = IncludeTranslationsByLanguage(languageCode, query);

        return await query
            .OrderBy(at => at.Code)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Подключает переводы типов счетов, при необходимости фильтруя по языку.
    /// </summary>
    /// <param name="languageCode">
    /// Код языка для фильтрации переводов. Если не указан, подключаются все.
    /// </param>
    /// <param name="query">Запрос к сущностям <see cref="AccountType"/>.</param>
    /// <returns>Запрос с подключенными переводами.</returns>
    private IQueryable<AccountType> IncludeTranslationsByLanguage(string? languageCode, IQueryable<AccountType> query)
    {
        return !string.IsNullOrWhiteSpace(languageCode)
            ? query.Include(at => at.Translations.Where(t => t.LanguageCode == languageCode))
            : query.Include(at => at.Translations);
    }
}
