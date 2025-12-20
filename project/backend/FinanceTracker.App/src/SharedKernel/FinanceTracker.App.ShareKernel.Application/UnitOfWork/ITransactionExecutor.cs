using FluentResults;

namespace FinanceTracker.App.ShareKernel.Application.UnitOfWork;

/// <summary>
/// Выполняет асинхронные операции в транзакционном контексте
/// с единообразной обработкой ошибок и результата выполнения.
/// </summary>
public interface ITransactionExecutor
{
    /// <summary>
    /// Выполняет асинхронную операцию с возвратом результата
    /// в транзакционном контексте с единообразной обработкой ошибок.
    /// </summary>
    /// <typeparam name="T">Dto тип результата операции.</typeparam>
    /// <param name="operation">Асинхронная операция для выполнения.</param>
    /// <param name="errorDescription">
    /// Описание ошибки, используемое при формировании результата в случае сбоя выполнения.
    /// </param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    public Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> operation,
        string errorDescription,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Выполняет асинхронную операцию без возврата результата
    /// в транзакционном контексте с единообразной обработкой ошибок.
    /// </summary>
    /// <param name="operation">Асинхронная операция для выполнения.</param>
    /// <param name="errorDescription">
    /// Описание ошибки, используемое при формировании результата в случае сбоя выполнения.
    /// </param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    Task<Result> ExecuteAsync(Func<Task> operation,
        string errorDescription,
        CancellationToken cancellationToken = default
    );
}
