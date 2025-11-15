using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Catalogs.Domain.Entities;

/// <summary>
/// Представляет страну в справочнике.
/// </summary>
public sealed class Country : CreatableEntity
{
    /// <summary>
    /// Название страны.
    /// </summary>
    public required string Name { get; set; }
}
