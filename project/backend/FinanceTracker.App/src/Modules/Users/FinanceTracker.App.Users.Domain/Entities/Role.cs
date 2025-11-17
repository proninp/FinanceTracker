using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Users.Domain.Entities;

/// <summary>
/// Роль пользователя в системе.
/// </summary>
public sealed class Role : SoftDeletableEntity, IActivableEtity
{
    /// <summary>
    /// Наименование роли.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Признак активности роли.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Признак того, что роль используется по умолчанию.
    /// </summary>
    public bool IsDefault { get; set; }
}
