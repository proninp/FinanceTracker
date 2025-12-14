using FinanceTracker.App.ShareKernel.Application.Data;

namespace FinanceTracker.App.ShareKernel.Application.UnitOfWork;

/// <summary>
/// Менеджер единицы работы (Unit of Work).
/// Отвечает за управление жизненным циклом и сохранением изменений.
/// </summary>
public interface IUnitOfWorkManager<TContext> where TContext : IDbContext
{
    /// <summary>
    /// Запускает новую единицу работы, в рамках которой отслеживаются изменения.
    /// </summary>
    void StartUnitOfWork();

    /// <summary>
    /// Показывает, была ли уже запущена единица работы.
    /// </summary>
    bool IsUnitOfWorkStarted { get; }

    /// <summary>
    /// Сохраняет все отслеживаемые изменения.
    /// </summary>
    /// <returns>Количество затронутых записей.</returns>
    int SaveChanges();

    /// <summary>
    /// Асинхронно сохраняет все отслеживаемые изменения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая операцию сохранения, с количеством затронутых записей.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
