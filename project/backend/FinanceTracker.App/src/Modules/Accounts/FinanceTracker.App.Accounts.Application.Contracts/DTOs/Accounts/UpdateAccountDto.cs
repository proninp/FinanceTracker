namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;

public sealed record UpdateAccountDto
{
    public required Guid Id { get; init; }
    public Guid? BankId { get; init; }
    public required string Name { get; init; }
    public decimal? CreditLimit { get; init; }
    public bool IsIncludeInBalance { get; init; }
    public bool IsDefault { get; init; }
}
