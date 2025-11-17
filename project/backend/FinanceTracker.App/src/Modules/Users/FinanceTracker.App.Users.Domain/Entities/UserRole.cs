using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Users.Domain.Entities;

/// <summary>
/// Связь пользователь - роль.
/// </summary>
public sealed class UserRole : BaseEntity
{
    /// <summary>
    /// Идентификатор роли.
    /// </summary>
    public required Guid RoleId { get; set; }

    /// <summary>
    /// Роль, назначенная пользователю.
    /// </summary>
    public required Role Role { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// Пользователь, которому назначена роль.
    /// </summary>
    public required User User { get; set; }
}
