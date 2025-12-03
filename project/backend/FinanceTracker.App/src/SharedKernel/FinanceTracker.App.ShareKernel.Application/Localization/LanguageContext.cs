using FinanceTracker.App.ShareKernel.Domain.Localization;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.App.ShareKernel.Application.Localization;

public sealed class LanguageContext : ILanguageContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LanguageContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Language CurrentLanguage { get; }

    public string CurrentLanguageCode => CurrentLanguage.ToCode();

    private Language DetermineLanguage()
    {
        var userLanguage = _httpContextAccessor
            .HttpContext?.User
            .FindFirst("language")?.Value;
        if (!string.IsNullOrEmpty(userLanguage))
            return LanguageCode.FromCode(userLanguage);

        var acceptLanguage = _httpContextAccessor.HttpContext?.Request
            .Headers["Accept-Language"].FirstOrDefault();
        if (!string.IsNullOrEmpty(acceptLanguage))
        {
            var languageCode = acceptLanguage // "en-US,en;q=0.9,ru;q=0.8"
                .Split(',')[0] // "en-US"
                .Split('-')[0] // "en"
                .Split(';')[0] // "en"
                .Trim();

            return LanguageCode.FromCode(languageCode);
        }

        return LanguageCode.FromCode(LanguageCode.Default);
    }
}
