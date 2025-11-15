namespace FinanceTracker.App.ShareKernel.Domain.Entities;

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
    /// Восстановить сущность — сбросить флаги и метаданные удаления.
    /// </summary>
    void Restore()
    {
        DeletedAt = null;
        DeletedBy = null;
        IsDeleted = false;
    }
}
