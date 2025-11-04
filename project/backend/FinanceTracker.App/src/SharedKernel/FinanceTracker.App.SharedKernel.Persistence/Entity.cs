namespace FinanceTracker.App.SharedKernel.Persistence;

/// <summary>
/// Базовый абстрактный класс для сущностей с полным набором функций:
/// идентификация, аудит и мягкое удаление.
/// </summary>
public abstract class Entity : SoftDeletableEntity
{
}
