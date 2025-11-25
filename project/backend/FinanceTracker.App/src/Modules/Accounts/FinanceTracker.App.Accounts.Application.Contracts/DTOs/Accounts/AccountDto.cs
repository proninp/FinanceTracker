namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;

public sealed record AccountDto
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required Guid AccountTypeId { get; init; }
    public required Guid CurrencyId { get; init; }
    public Guid? BankId { get; init; }
    public required string Name { get; init; }
    public decimal? CreditLimit { get; init; }
    public bool IsIncludeInBalance { get; init; }
    public bool IsDefault { get; init; }
    public bool IsArchived { get; init; }
}
