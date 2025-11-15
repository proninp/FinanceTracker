namespace FinanceTracker.App.ShareKernel.Domain.Events;

/// <summary>
/// Базовое событие предметной области.
/// </summary>
public abstract class DomainEventBase
{
    /// <summary>
    /// Время создания события в UTC.
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Возвращает ключ сообщения, идентифицирующий событие.
    /// </summary>
    /// <returns>Строка с уникальным ключом события.</returns>
    public abstract string GetMessageKey();

    /// <summary>
    /// Возвращает название топика, в который должно быть опубликовано событие.
    /// </summary>
    /// <returns>Строка с именем топика.</returns>
    public abstract string GetTopic();
}
