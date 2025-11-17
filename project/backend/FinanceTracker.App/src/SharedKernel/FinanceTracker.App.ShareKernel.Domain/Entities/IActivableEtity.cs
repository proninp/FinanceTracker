namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Интерфейс для сущностей, поддерживающих состояние активности.
/// </summary>
public interface IActivableEtity
{
    /// <summary>
    /// Признак активности сущности.
    /// </summary>
    bool IsActive { get; set; }
}
