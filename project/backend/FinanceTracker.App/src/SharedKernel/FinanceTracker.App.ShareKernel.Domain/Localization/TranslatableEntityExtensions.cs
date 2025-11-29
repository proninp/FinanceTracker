namespace FinanceTracker.App.ShareKernel.Domain.Localization;

/// <summary>
/// Extension-методы для работы с переводимыми сущностями.
/// </summary>
public static class TranslatableEntityExtensions
{
    /// <summary>
    /// Получить перевод для указанного языка или значение по умолчанию.
    /// </summary>
    /// <typeparam name="TTranslation">Тип класса перевода.</typeparam>
    /// <param name="translations">Коллекция переводов.</param>
    /// <param name="languageCode">Код языка (ISO 639-1).</param>
    /// <param name="selector">Селектор для извлечения переводимого значения.</param>
    /// <param name="defaultValue">Значение по умолчанию, если перевод не найден.</param>
    /// <returns>Переведенное значение или значение по умолчанию.</returns>
    public static string GetTranslation<TTranslation>(
        this IEnumerable<TTranslation> translations,
        string languageCode,
        Func<TTranslation, string> selector,
        string defaultValue)
        where TTranslation : TranslationBase<Guid>
    {
        var translation = translations.FirstOrDefault(t => t.LanguageCode == languageCode);
        return translation != null ? selector(translation) : defaultValue;
    }

    /// <summary>
    /// Получить перевод для указанного языка или значение по умолчанию.
    /// </summary>
    public static string GetTranslation<TTranslation>(
        this IEnumerable<TTranslation> translations,
        Language language,
        Func<TTranslation, string> selector,
        string defaultValue)
        where TTranslation : TranslationBase<Guid>
    {
        return translations.GetTranslation(language.ToCode(), selector, defaultValue);
    }
}