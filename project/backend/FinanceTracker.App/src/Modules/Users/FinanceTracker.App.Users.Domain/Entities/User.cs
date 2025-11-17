using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Users.Domain.Entities;

/// <summary>
/// Пользователь системы.
/// </summary>
public sealed class User : SoftDeletableEntity, IActivableEtity
{
    /// <summary>
    /// Идентификатор роли пользователя.
    /// </summary>
    public required Guid RoleId { get; set; }

    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public required Role Role { get; set; }

    /// <summary>
    /// Отображаемое имя пользователя.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Признак активности пользователя.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Идентификатор часового пояса по умолчанию.
    /// </summary>
    public required Guid DefaultTimeZoneId { get; set; }

    /// <summary>
    /// Часовой пояс пользователя по умолчанию.
    /// </summary>
    public required TimeZone DefaultTimeZone { get; set; }
}
