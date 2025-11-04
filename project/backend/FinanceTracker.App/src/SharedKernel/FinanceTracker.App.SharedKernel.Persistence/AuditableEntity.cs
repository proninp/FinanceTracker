using System.ComponentModel.DataAnnotations.Schema;
using FinanceTracker.App.SharedKernel.Contracts.Persistence;

namespace FinanceTracker.App.SharedKernel.Persistence;

/// <summary>
/// Базовый абстрактный класс для сущностей с аудитом создания и обновления.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity<Guid>, IEntity<Guid>
{
    public Guid Id { get; init; }

    [Column("created_at")]
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("updated_by")]
    public Guid? UpdatedBy { get; set; }
}
