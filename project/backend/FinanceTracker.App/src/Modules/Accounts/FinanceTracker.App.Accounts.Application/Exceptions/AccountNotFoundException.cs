namespace FinanceTracker.App.Accounts.Application.Exceptions;

public sealed class AccountNotFoundException : AccountsApplicationException
{
    public Guid AccountId { get; }

    public AccountNotFoundException(Guid accountId)
        : base($"Account with ID '{accountId}' was not found.")
    {
        AccountId = accountId;
    }
}
