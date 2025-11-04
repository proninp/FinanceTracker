using FinanceTracker.App.SharedKernel.Contracts.Persistence;

namespace FinanceTracker.App.SharedKernel.Contracts.Events;

/// <summary>
/// Интерфейс для сущностей, содержащих события предметной области.
/// </summary>
public interface IEntityWithDomainEvents : IEntity
{
    /// <summary>
    /// Коллекция событий предметной области, связанных с сущностью.
    /// </summary>
    IReadOnlyList<DomainEventBase> DomainEvents { get; }

    /// <summary>
    /// Возвращает текущие события предметной области и очищает коллекцию.
    /// </summary>
    /// <returns>Список событий предметной области, которые были зарегистрированы.</returns>
    IReadOnlyList<DomainEventBase> GetAndClearDomainEvents();

    /// <summary>
    /// Добавляет новое событие предметной области в коллекцию сущности.
    /// </summary>
    /// <param name="domainEvent">Событие предметной области для добавления.</param>
    void AddDomainEvent(DomainEventBase domainEvent);

    /// <summary>
    /// Удаляет указанное событие предметной области из коллекции сущности.
    /// </summary>
    /// <param name="domainEvent">Событие предметной области, которое требуется удалить.</param>
    void RemoveDomainEvent(DomainEventBase domainEvent);

    /// <summary>
    /// Очищает все события предметной области, связанные с сущностью.
    /// </summary>
    void ClearDomainEvents();
}
