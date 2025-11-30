namespace FinanceTracker.App.ShareKernel.Domain.Localization;

/// <summary>
/// Константы для кодов языков в формате ISO 639-1.
/// </summary>
public static class LanguageCode
{
    /// <summary>
    /// Русский язык.
    /// </summary>
    public const string Russian = "ru";

    /// <summary>
    /// Английский язык.
    /// </summary>
    public const string English = "en";

    /// <summary>
    /// Язык по умолчанию.
    /// </summary>
    public const string Default = Russian;

    /// <summary>
    /// Преобразует enum Language в строковый код.
    /// </summary>
    public static string ToCode(this Language language) => language switch
    {
        Language.Russian => Russian,
        Language.English => English,
        _ => Default
    };

    /// <summary>
    /// Преобразует строковый код в enum Language.
    /// </summary>
    public static Language FromCode(string? code) => code?.ToLowerInvariant() switch
    {
        Russian => Language.Russian,
        English => Language.English,
        _ => Language.Russian
    };
}
