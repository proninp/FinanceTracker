using FinanceTracker.App.ShareKernel.Application.Errors;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.App.SharedKernel.WebApi.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Преобразует Result в IActionResult
    /// </summary>
    public static IActionResult ToActionResult(this Result result)
    {
        return result.IsSuccess
            ? new OkResult()
            : MapErrorToActionResult(result.Errors[0]);
    }

    /// <summary>
    /// Преобразует Result&lt;T&gt; в IActionResult с обработкой случая, когда результат null
    /// </summary>
    public static IActionResult ToActionResult<T>(this Result<T> result, Func<T, IActionResult>? onSuccess = null)
    {
        return result.IsSuccess
            ? onSuccess is null
                ? new OkObjectResult(result.Value)
                : onSuccess(result.Value)
            : MapErrorToActionResult(result.Errors[0]);
    }

    /// <summary>
    /// Преобразует Result в CreatedAtAction для POST запросов
    /// </summary>
    public static IActionResult ToCreatedActionResult<T>(this Result<T> result, string actionName,
        object? routeValues = null
    )
    {
        return result.IsSuccess
            ? new CreatedAtActionResult(actionName, null, routeValues, result.Value)
            : MapErrorToActionResult(result.Errors[0]);
    }

    /// <summary>
    /// Преобразует Result в NoContent для DELETE/PUT запросов
    /// </summary>
    public static IActionResult ToNoContentResult(this Result result)
    {
        return result.IsSuccess
            ? new NoContentResult()
            : MapErrorToActionResult(result.Errors[0]);
    }

    /// <summary>
    /// Расширение для ValidationError с детализацией ошибок валидации
    /// </summary>
    public static IActionResult ToValidationProblemResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        var appError = result.Errors.FirstOrDefault() as AppError;

        if (appError?.ErrorCode == AppErrorCode.ValidationError)
        {
            ValidationProblemDetails validationProblemDetails;
            if (appError.Metadata is not null)
            {
                var validationErrors = new Dictionary<string, string[]>();
                foreach (var (key, value) in appError.Metadata)
                {
                    validationErrors[key] = [value.ToString() ?? string.Empty];
                }

                validationProblemDetails = new ValidationProblemDetails(validationErrors);
            }
            else
            {
                validationProblemDetails = new ValidationProblemDetails();
            }

            validationProblemDetails.Title = "Validation Error";
            validationProblemDetails.Detail = appError.Message;
            validationProblemDetails.Status = StatusCodes.Status400BadRequest;
            validationProblemDetails.Type = "https://httpstatuses.com/400";

            return new BadRequestObjectResult(validationProblemDetails);
        }

        return MapErrorToActionResult(appError ?? new AppError(result.Errors[0].Message, AppErrorCode.UnexpectedError));
    }

    /// <summary>
    /// Маппинг AppError на соответствующий IActionResult с RFC 7807 ProblemDetails
    /// </summary>
    private static IActionResult MapErrorToActionResult(IError error)
    {
        if (error is not AppError appError)
        {
            return GetUnexpectedResult(error.Message);
        }

        var (statusCode, title) = appError.ErrorCode switch
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
            Detail = appError.Message,
            Status = statusCode,
            Type = $"https://httpstatuses.com/{statusCode}"
        };

        if ((appError.Metadata?.Count ?? 0) > 0)
        {
            problemDetails.Extensions["errors"] = appError.Metadata;
        }

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode,
        };
    }

    private static IActionResult GetUnexpectedResult(string message)
    {
        return new ObjectResult(new ProblemDetails
        {
            Title = "Unexpected Error",
            Detail = message,
            Status = StatusCodes.Status500InternalServerError,
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
