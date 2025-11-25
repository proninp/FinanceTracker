namespace FinanceTracker.App.Accounts.Application.Exceptions;

public sealed class AccountTypeCodeAlreadyExistsException : AccountsApplicationException
{
    public string Code { get; }

    public AccountTypeCodeAlreadyExistsException(string code)
        : base($"Account type with code '{code}' already exists.")
    {
        Code = code;
    }
}
