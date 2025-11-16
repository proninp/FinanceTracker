namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Базовый абстрактный класс для сущностей с аудитом создания и обновления.
/// </summary>
public abstract class AuditableEntity : CreatableEntity, IUpdatableEntity<Guid>
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public Guid? UpdatedBy { get; set; }
}
