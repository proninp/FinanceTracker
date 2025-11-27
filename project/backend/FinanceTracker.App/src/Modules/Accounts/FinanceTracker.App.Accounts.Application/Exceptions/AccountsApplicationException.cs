namespace FinanceTracker.App.Accounts.Application.Exceptions;

public abstract class AccountsApplicationException : Exception
{
    protected AccountsApplicationException(string message) : base(message)
    {
    }

    protected AccountsApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
