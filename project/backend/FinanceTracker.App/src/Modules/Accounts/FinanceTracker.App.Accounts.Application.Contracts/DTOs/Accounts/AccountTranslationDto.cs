using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FinanceTracker.App.Accounts.Domain.Entities;
using FinanceTracker.App.ShareKernel.Application.Localization;

namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;

/// <summary>
/// DTO перевода наименования счёта на определённый язык.
/// </summary>
public sealed record AccountTranslationDto : TranslationDto
{
    /// <summary>
    /// Наименование счёта на соответствующем языке.
    /// </summary>
    public required string Name { get; init; }
}

/// <summary>
/// Методы расширения для преобразования DTO перевода типа счёта в доменную модель.
/// </summary>
public static class AccountTranslationDtoExtensions
{
    /// <summary>
    /// Преобразует DTO перевода типа счёта в доменную модель перевода.
    /// </summary>
    /// <param name="dto">
    /// DTO перевода типа счёта.
    /// </param>
    /// <param name="accountId">
    /// Идентификатор типа счёта, к которому относится перевод.
    /// </param>
    /// <returns>
    /// Доменная модель перевода типа счёта.
    /// </returns>
    public static AccountTypeTranslation ToModel(this AccountTypeTranslationDto dto, Guid accountId)
    {
        return new AccountTypeTranslation
        {
            EntityId = accountId,
            LanguageCode = dto.LanguageCode,
            Description = dto.Description.Trim()
        };
    }

    public static AccountTranslation ToModel(this AccountTranslationDto dto, Guid accountId)
    {
        return new AccountTranslation
        {
            EntityId = accountId,
            LanguageCode = dto.LanguageCode,
            Name = dto.Name.Trim()
        };
    }
}
