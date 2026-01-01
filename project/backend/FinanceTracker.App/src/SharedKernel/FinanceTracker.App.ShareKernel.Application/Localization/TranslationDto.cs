namespace FinanceTracker.App.ShareKernel.Application.Localization;

/// <summary>
/// Абстрактная сущность DTO для перевода типа счёта на определённый язык.
/// </summary>
public abstract record TranslationDto
{
    /// <summary>
    /// Код языка (ISO 639-1), например: "ru", "en".
    /// </summary>
    public required string LanguageCode { get; init; }
}

/// <summary>
/// Методы расширения для проверки коллекций переводов.
/// </summary>
public static class TranslationDtoExtensions
{
    /// <summary>
    /// Проверяет коллекцию переводов на наличие дублирующихся кодов языков.
    /// </summary>
    /// <typeparam name="TTrabslation">
    /// Тип перевода, наследующий <see cref="TranslationDto"/>.
    /// </typeparam>
    /// <param name="dtos">
    /// Коллекция переводов для проверки.
    /// </param>
    /// <param name="duplicateLanguages">
    /// Строка с кодами языков, для которых обнаружены дубликаты.
    /// Если метод возвращает <c>false</c>, значением будет пустая строка.
    /// </param>
    /// <returns>
    /// <c>true</c>, если найдены дублирующиеся коды языков; иначе <c>false</c>.
    /// </returns>
    public static bool CheckDuplicates<TTrabslation>(this IEnumerable<TTrabslation> dtos, out string duplicateLanguages)
        where TTrabslation : TranslationDto
    {
        duplicateLanguages = string.Empty;
        var duplicates = dtos
            .GroupBy(t => t.LanguageCode)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicates.Count != 0)
        {
            duplicateLanguages = string.Join(", ", duplicates);
            return true;
        }

        return false;
    }
}
