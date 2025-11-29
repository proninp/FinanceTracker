namespace FinanceTracker.App.ShareKernel.Domain.Localization;

/// <summary>
/// Базовый класс для сущностей переводов.
/// </summary>
/// <typeparam name="TEntityId">Тип идентификатора родительской сущности.</typeparam>
public abstract class TranslationBase<TEntityId> where TEntityId : struct
{
    /// <summary>
    /// Идентификатор родительской сущности.
    /// </summary>
    public required TEntityId EntityId { get; set; }

    /// <summary>
    /// Код языка (ISO 639-1).
    /// </summary>
    public required string LanguageCode { get; set; }
}