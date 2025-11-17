using FinanceTracker.App.ShareKernel.Domain.Entities;

namespace FinanceTracker.App.Users.Domain.Entities;

/// <summary>
/// Информация о часовом поясе.
/// </summary>
public sealed class TimeZone : BaseEntity
{
    /// <summary>
    /// Название часового пояса в формате IANA.
    /// </summary>
    public required string IanaName { get; set; }

    /// <summary>
    /// Отображаемое имя часового пояса.
    /// </summary>
    public required string Name { get; set; }
}
