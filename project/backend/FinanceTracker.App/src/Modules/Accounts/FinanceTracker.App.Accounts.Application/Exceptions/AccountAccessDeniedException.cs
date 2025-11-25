namespace FinanceTracker.App.Accounts.Application.Exceptions;

public sealed class AccountAccessDeniedException : AccountsApplicationException
{
    public Guid AccountId { get; }
    public Guid UserId { get; }

    public AccountAccessDeniedException(Guid accountId, Guid userId)
        : base($"User '{userId}' does not have access to account '{accountId}'.")
    {
        AccountId = accountId;
        UserId = userId;
    }
}
