namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Базовый абстрактный класс для сущностей с аудитом создания.
/// </summary>
public abstract class CreatableEntity : BaseEntity, ICreatableEntity<Guid>
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public Guid? CreatedBy { get; set; }
}
