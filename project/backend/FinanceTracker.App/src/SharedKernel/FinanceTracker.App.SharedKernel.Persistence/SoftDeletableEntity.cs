using System.ComponentModel.DataAnnotations.Schema;
using FinanceTracker.App.SharedKernel.Contracts.Persistence;

namespace FinanceTracker.App.SharedKernel.Persistence;

/// <summary>
/// Базовый абстрактный класс для сущностей с поддержкой мягкого удаления и аудита.
/// </summary>
public abstract class SoftDeletableEntity : AuditableEntity, ISoftDeletableEntity<Guid>
{
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    public virtual void Restore()
    {
        DeletedAt = null;
        DeletedBy = null;
        IsDeleted = false;
    }
}
