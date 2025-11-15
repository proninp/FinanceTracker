namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Базовый абстрактный класс для сущностей с поддержкой мягкого удаления и аудита.
/// </summary>
public abstract class SoftDeletableEntity : AuditableEntity, ISoftDeletableEntity<Guid>
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public bool IsDeleted { get; set; }
}
