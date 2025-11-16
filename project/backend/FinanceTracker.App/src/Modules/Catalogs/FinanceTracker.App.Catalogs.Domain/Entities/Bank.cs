using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Catalogs.Domain.Entities;

/// <summary>
/// Информация о банке.
/// </summary>
public sealed class Bank : CreatableEntity
{
    /// <summary>
    /// Идентификатор страны.
    /// </summary>
    public required Guid CountryId { get; set; }

    /// <summary>
    /// Страна, к которой относится банк.
    /// </summary>
    public required Country Country { get; set; }

    /// <summary>
    /// Наименование банка.
    /// </summary>
    public required string Name { get; set; }
}
