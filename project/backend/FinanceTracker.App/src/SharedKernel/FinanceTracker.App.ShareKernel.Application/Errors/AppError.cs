using FluentResults;

namespace FinanceTracker.App.ShareKernel.Application.Errors;

public class AppError : Error
{
    public AppErrorCode ErrorCode { get; }

    public AppError(string message, AppErrorCode errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public static AppError NotFound(string? message = null) =>
        new AppError(message ?? "Resource was not found.", AppErrorCode.NotFound);

    public static AppError Forbidden(string? message = null) =>
        new AppError(message ?? "Operation is forbidden.", AppErrorCode.Forbidden);

    public static AppError Validation(string message) =>
        new AppError(message, AppErrorCode.ValidationError);

    public static AppError Conflict(string message) =>
        new AppError(message, AppErrorCode.Conflict);

    public static AppError Unexpected(string message) =>
        new AppError(message, AppErrorCode.UnexpectedError);
}
