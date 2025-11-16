namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Интерфейс для сущностей с аудитом создания.
/// </summary>
public interface ICreatableEntity<TKey>
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
}
