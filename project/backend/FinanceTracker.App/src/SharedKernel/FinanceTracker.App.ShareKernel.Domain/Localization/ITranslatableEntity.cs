namespace FinanceTracker.App.ShareKernel.Domain.Localization;

/// <summary>
/// Интерфейс для сущностей, поддерживающих мультиязычность.
/// </summary>
/// <typeparam name="TTranslation">Тип класса перевода.</typeparam>
public interface ITranslatableEntity<TTranslation>
{
    /// <summary>
    /// Коллекция переводов для разных языков.
    /// </summary>
    ICollection<TTranslation> Translations { get; set; }
}