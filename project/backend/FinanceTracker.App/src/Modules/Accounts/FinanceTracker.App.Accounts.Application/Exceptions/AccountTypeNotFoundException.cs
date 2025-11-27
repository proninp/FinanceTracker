namespace FinanceTracker.App.Accounts.Application.Exceptions;

public sealed class AccountTypeNotFoundException : AccountsApplicationException
{
    public Guid AccountTypeId { get; }

    public AccountTypeNotFoundException(Guid accountTypeId)
        : base($"Account type with ID '{accountTypeId}' was not found.")
    {
        AccountTypeId = accountTypeId;
    }

    public AccountTypeNotFoundException(string code)
        : base($"Account type with code '{code}' was not found.")
    {
    }
}
