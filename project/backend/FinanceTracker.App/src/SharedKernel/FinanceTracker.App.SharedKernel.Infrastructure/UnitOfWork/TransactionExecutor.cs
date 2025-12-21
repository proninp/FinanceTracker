using FinanceTracker.App.ShareKernel.Application.Errors;
using FinanceTracker.App.ShareKernel.Application.UnitOfWork;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.App.SharedKernel.Infrastructure.UnitOfWork;

/// <summary>
/// <inheritdoc/>
/// </summary>
public abstract class TransactionExecutor(IUnitOfWorkManager unitOfWorkManager, ILogger logger) : ITransactionExecutor
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="operation"><inheritdoc/></param>
    /// <param name="informationLogDescription"><inheritdoc/></param>
    /// <param name="errorDescription"><inheritdoc/></param>
    /// <param name="errorLogDescription"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <typeparam name="T"><inheritdoc/></typeparam>
    /// <returns><inheritdoc/></returns>
    public async Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> operation,
        string? informationLogDescription,
        string errorDescription,
        string errorLogDescription,
        CancellationToken cancellationToken = default
    )
    {
        unitOfWorkManager.StartUnitOfWork();
        try
        {
            var result = await operation();
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(informationLogDescription) && logger.IsEnabled(LogLevel.Information))
                logger.LogInformation(informationLogDescription);
            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, errorDescription);
            return AppError.Unexpected(errorDescription);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="operation"><inheritdoc/></param>
    /// <param name="informationLogDescription"><inheritdoc/></param>
    /// <param name="errorDescription"><inheritdoc/></param>
    /// <param name="errorLogDescription"><inheritdoc/></param>
    /// <param name="cancellationToken"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Result> ExecuteAsync(Func<Task> operation,
        string? informationLogDescription,
        string errorDescription,
        string errorLogDescription,
        CancellationToken cancellationToken = default
    )
    {
        unitOfWorkManager.StartUnitOfWork();
        try
        {
            await operation();
            await unitOfWorkManager.SaveChangesAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(informationLogDescription) && logger.IsEnabled(LogLevel.Information))
                logger.LogInformation(informationLogDescription);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, errorDescription);
            return AppError.Unexpected(errorDescription);
        }
    }
}
