namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Интерфейс для сущностей с аудитом обновления.
/// </summary>
public interface IUpdatableEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Дата и время последнего обновления сущности.
    /// </summary>
    DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя, обновившего сущность.
    /// </summary>
    TKey? UpdatedBy { get; set; }
}
