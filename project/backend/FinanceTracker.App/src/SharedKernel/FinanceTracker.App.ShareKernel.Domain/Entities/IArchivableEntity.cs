namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Интерфейс для сущностей, которые могут быть помечены как архивированные.
/// </summary>
public interface IArchivableEntity
{
    /// <summary>
    /// Признак того, что сущность перенесена в архив (true — в архиве, false — активна).
    /// </summary>
    bool IsArchived { get; set; }
}
