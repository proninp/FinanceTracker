namespace FinanceTracker.App.ShareKernel.Application.Errors;

/// <summary>
/// Коды ошибок приложения
/// </summary>
public enum AppErrorCode
{
    /// <summary>
    /// Ресурс не найден
    /// </summary>
    NotFound,

    /// <summary>
    /// Доступ запрещен
    /// </summary>
    Forbidden,

    /// <summary>
    /// Ошибка валидации данных
    /// </summary>
    ValidationError,

    /// <summary>
    /// Конфликт данных
    /// </summary>
    Conflict,

    /// <summary>
    /// Непредвиденная ошибка
    /// </summary>
    UnexpectedError
}
