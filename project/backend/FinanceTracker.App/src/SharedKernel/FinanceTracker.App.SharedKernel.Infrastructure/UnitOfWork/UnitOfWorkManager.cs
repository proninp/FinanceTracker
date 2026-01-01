using FinanceTracker.App.ShareKernel.Application.Data;
using FinanceTracker.App.ShareKernel.Application.UnitOfWork;

namespace FinanceTracker.App.SharedKernel.Infrastructure.UnitOfWork;

/// <summary>
/// <inheritdoc/>
/// </summary>
/// <param name="context">Экземпляр контекста базы данных, используемый менеджером для выполнения операций сохранения.</param>
/// <typeparam name="TContext">Тип контекста базы данных, реализует <see cref="IDbContext"/>.</typeparam>
public abstract class UnitOfWorkManager<TContext>(TContext context) : IUnitOfWorkManager
    where TContext : IDbContext
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void StartUnitOfWork()
    {
        IsUnitOfWorkStarted = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsUnitOfWorkStarted { get; private set; } = false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns><inheritdoc/></returns>
    public int SaveChanges() =>
        context.SaveChanges();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}
