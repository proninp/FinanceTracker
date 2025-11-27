namespace FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;

public sealed record CreateAccountTypeDto
{
    public required string Code { get; init; }
    public required string Description { get; init; }
}
