namespace FinanceTracker.App.ShareKernel.Application.Data;

/// <summary>
/// Базовый интерфейс для контекста базы данных,
/// предоставляющий операции сохранения изменений.
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Сохраняет все изменения, отслеживаемые контекстом.
    /// </summary>
    /// <returns>Количество затронутых записей.</returns>
    int SaveChanges();

    /// <summary>
    /// Асинхронно сохраняет все изменения, отслеживаемые контекстом.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Задача, представляющая операцию сохранения, результатом которой является количество затронутых записей.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
