using FinanceTracker.App.ShareKernel.Application.Errors;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.App.SharedKernel.WebApi.Extensions;

public static class ResultExtesions
{
    public static IActionResult ToActionResult(this Result result)
    {
        throw new  NotImplementedException();
    }

    private static IActionResult MapErrorToActionResult(AppError error)
    {
        var (statusCode, title) = error.ErrorCode switch
        {
            AppErrorCode.NotFound => (StatusCodes.Status404NotFound, "Resource Not Found"),
            AppErrorCode.Forbidden => (StatusCodes.Status403Forbidden, "Forbidden"),
            AppErrorCode.ValidationError => (StatusCodes.Status400BadRequest, "Validation Error"),
            AppErrorCode.Conflict => (StatusCodes.Status409Conflict, "Conflict"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error"),
        };

        var problemDetails = new ProblemDetails
        {
            Title = title,
            Detail = error.Message,
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}"
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode,
        };
    }
}
