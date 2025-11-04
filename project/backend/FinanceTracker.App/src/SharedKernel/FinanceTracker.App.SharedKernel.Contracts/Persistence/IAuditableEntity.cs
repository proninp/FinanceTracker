namespace FinanceTracker.App.SharedKernel.Contracts.Persistence;

/// <summary>
/// Интерфейс для сущностей с аудитом создания и обновления.
/// </summary>
public interface IAuditableEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Дата и время создания сущности.
    /// </summary>
    DateTime CreatedAt { get; init; }

    /// <summary>
    /// Идентификатор пользователя, создавшего сущность.
    /// </summary>
    TKey? CreatedBy { get; set; }

    /// <summary>
    /// Дата и время последнего обновления сущности.
    /// </summary>
    DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя, обновившего сущность.
    /// </summary>
    TKey? UpdatedBy { get; set; }
}
