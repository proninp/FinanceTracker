namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Интерфейс для сущностей с аудитом создания и обновления.
/// </summary>
public interface IAuditableEntity<TKey> : ICreatableEntity<TKey>, IUpdatableEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
}
