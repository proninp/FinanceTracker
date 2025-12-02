using FinanceTracker.App.Accounts.Application.Contracts.Repositories;
using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.Infrastructure.EntityFramework;
using FinanceTracker.App.ShareKernel.Application.Pagination;
using FinanceTracker.App.ShareKernel.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.App.Infrastructure.Repositories.Repositories;

/// <summary>
/// Реализация репозитория для работы со счетами.
/// </summary>
internal sealed class AccountRepository(
    AccountsDbContext context,
    IUnitOfWorkManager<AccountsDbContext> unitOfWorkManager
) : IAccountRepository
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Include(a => a.AccountType)
            .ThenInclude(at => at.Translations)
            .Include(a => a.Translations)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <param name="userId"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Account?> GetByIdForUserAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Include(a => a.AccountType)
            .ThenInclude(at => at.Translations)
            .Include(a => a.Translations)
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="settings"><inheritdoc/></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<PaginationResult<Account>> GetPagedAsync(
        PaginationSettings settings,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.Accounts.AsQueryable();

        if (!includeArchived)
        {
            query = query.Where(a => !a.IsArchived);
        }

        query = query
            .Include(a => a.Translations)
            .Include(a => a.AccountType)
            .ThenInclude(at => at.Translations);

        var totalCount = await query.CountAsync(cancellationToken);

        if (totalCount == 0)
        {
            return PaginationResult<Account>.Empty(settings.PageNumber, settings.EffectivePageSize);
        }

        var data = await query
            .OrderByDescending(a => a.CreatedAt)
            .Skip(settings.Skip)
            .Take(settings.EffectivePageSize)
            .ToListAsync(cancellationToken);

        return new PaginationResult<Account>(data, settings, totalCount);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="userId"><inheritdoc/></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<IReadOnlyList<Account>> GetUserAccountsAsync(
        Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.Accounts
            .Include(a => a.AccountType)
            .ThenInclude(at => at.Translations)
            .Include(a => a.Translations)
            .Where(a => a.UserId == userId);

        if (!includeArchived)
        {
            query = query.Where(a => !a.IsArchived);
        }

        return await query
            .OrderBy(a => a.Name)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="userId"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Account?> GetDefaultAccountForUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Include(a => a.AccountType)
            .ThenInclude(at => at.Translations)
            .Include(a => a.Translations)
            .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .AnyAsync(a => a.Id == id, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="accountId"><inheritdoc/></param>
    /// <param name="userId"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<bool> IsUserAccountOwnerAsync(Guid accountId, Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .AnyAsync(a => a.Id == accountId && a.UserId == userId, cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="account"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        await context.Accounts.AddAsync(account, cancellationToken);
        if (!unitOfWorkManager.IsUnitOfWorkStarted)
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
        return account;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="account"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        context.Accounts.Update(account);
        if (!unitOfWorkManager.IsUnitOfWorkStarted)
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="account"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    public async Task DeleteAsync(Account account, CancellationToken cancellationToken = default)
    {
        context.Accounts.Remove(account);
        if (!unitOfWorkManager.IsUnitOfWorkStarted)
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="userId"><inheritdoc/></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns></returns>
    public async Task<int> CountUserAccountsAsync(Guid userId, bool includeArchived = false, CancellationToken cancellationToken = default)
    {
        var query = context.Accounts
            .Where(a => a.UserId == userId);

        if (!includeArchived)
        {
            query = query.Where(a => !a.IsArchived);
        }

        return await query.CountAsync(cancellationToken);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="userId"><inheritdoc/></param>
    /// <param name="includeArchived"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<IReadOnlyList<Account>> GetUserAccountsIncludingDeletedAsync(
        Guid userId,
        bool includeArchived = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = context.Accounts
            .IgnoreQueryFilters()
            .Include(a => a.AccountType)
            .ThenInclude(at => at.Translations)
            .Include(a => a.Translations)
            .Where(a => a.UserId == userId);

        if (!includeArchived)
        {
            query = query.Where(a => !a.IsArchived);
        }

        return await query
            .OrderBy(a => a.Name)
            .ToListAsync(cancellationToken);
    }
}
