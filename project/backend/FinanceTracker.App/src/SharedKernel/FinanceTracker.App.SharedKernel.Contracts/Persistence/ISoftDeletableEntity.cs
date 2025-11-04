namespace FinanceTracker.App.SharedKernel.Contracts.Persistence;

/// <summary>
/// Интерфейс для сущностей с поддержкой мягкого удаления.
/// </summary>
public interface ISoftDeletableEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    /// <summary>
    /// Дата и время удаления сущности.
    /// </summary>
    DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя, удалившего сущность.
    /// </summary>
    TKey? DeletedBy { get; set; }

    /// <summary>
    /// Флаг, указывающий, удалена ли сущность.
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Восстанавливает удаленную сущность.
    /// </summary>
    void Restore();
}
