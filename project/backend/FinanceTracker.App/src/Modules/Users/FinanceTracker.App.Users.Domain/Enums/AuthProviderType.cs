namespace FinanceTracker.App.Users.Domain.Enums;

/// <summary>
/// Тип провайдера аутентификации.
/// </summary>
public enum AuthProviderType
{
    /// <summary>
    /// Аутентификация по email и паролю.
    /// </summary>
    Email,

    /// <summary>
    /// Аутентификация через Telegram.
    /// </summary>
    Telegram,

    /// <summary>
    /// Аутентификация через внешний OAuth2-провайдер.
    /// </summary>
    OAuth2,

    /// <summary>
    /// Аутентификация через Google.
    /// </summary>
    Google,

    /// <summary>
    /// Аутентификация через GitHub.
    /// </summary>
    Github,

    /// <summary>
    /// Аутентификация через Apple.
    /// </summary>
    Apple
}
