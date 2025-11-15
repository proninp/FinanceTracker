namespace FinanceTracker.App.ShareKernel.Domain.Entities;

/// <summary>
/// Базовый абстрактный класс для сущностей с полным набором функций:
/// идентификация, аудит и мягкое удаление.
/// </summary>
public abstract class Entity : SoftDeletableEntity
{
}
