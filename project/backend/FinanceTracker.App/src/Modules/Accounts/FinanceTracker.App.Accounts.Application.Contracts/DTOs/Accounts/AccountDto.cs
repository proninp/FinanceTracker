using FinanceTracker.App.Accounts.Domain.Entities;

namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;

/// <summary>
/// DTO, представляющий счёт пользователя.
/// </summary>
public sealed record AccountDto
{
    /// <summary>
    /// Идентификатор счёта.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Идентификатор пользователя, владельца счёта.
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// Идентификатор типа счёта.
    /// </summary>
    public required Guid AccountTypeId { get; init; }

    /// <summary>
    /// Идентификатор валюты счёта.
    /// </summary>
    public required Guid CurrencyId { get; init; }

    /// <summary>
    /// Идентификатор банка (если применимо).
    /// </summary>
    public Guid? BankId { get; init; }

    /// <summary>
    /// Наименование счёта.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Кредитный лимит по счёту (если применимо).
    /// </summary>
    public decimal? CreditLimit { get; init; }

    /// <summary>
    /// Признак включения счёта в общий баланс.
    /// </summary>
    public bool IsIncludeInBalance { get; init; }

    /// <summary>
    /// Признак счёта по умолчанию.
    /// </summary>
    public bool IsDefault { get; init; }

    /// <summary>
    /// Признак архивного счёта.
    /// </summary>
    public bool IsArchived { get; init; }
}

/// <summary>
/// Методы расширения для преобразования счёта в DTO.
/// </summary>
public static class AccountDtoExtensions
{
    /// <summary>
    /// Преобразует доменную модель счёта в DTO с учётом языка.
    /// </summary>
    public static AccountDto ToDto(this Account account, string languageCode)
    {
        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            AccountTypeId = account.AccountTypeId,
            CurrencyId = account.CurrencyId,
            BankId = account.BankId,
            Name = account.GetName(languageCode),
            CreditLimit = account.CreditLimit,
            IsIncludeInBalance = account.IsIncludeInBalance,
            IsDefault = account.IsDefault,
            IsArchived = account.IsArchived
        };
    }

    /// <summary>
    /// Преобразует перечисление доменных моделей счёта в список DTO с учётом языка.
    /// </summary>
    public static List<AccountDto> ToDto(this IEnumerable<Account> accounts, string languageCode) =>
        accounts
            .Select(a => a.ToDto(languageCode))
            .ToList();
}
