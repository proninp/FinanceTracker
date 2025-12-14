using FinanceTracker.App.ShareKernel.Application.Localization;
using FinanceTracker.App.ShareKernel.Domain.Localization;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.App.SharedKernel.WebApi.Localization;

internal sealed class HttpLanguageContext : ILanguageContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Lazy<Language> _currentLanguage;

    public HttpLanguageContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _currentLanguage = new Lazy<Language>(DetermineLanguage);
    }

    public Language CurrentLanguage => _currentLanguage.Value;

    public string CurrentLanguageCode => CurrentLanguage.ToCode();

    private Language DetermineLanguage()
    {
        var userLanguage = _httpContextAccessor
            .HttpContext?.User
            .FindFirst("language")?.Value;
        if (!string.IsNullOrEmpty(userLanguage))
        {
            return LanguageCode.FromCode(userLanguage);
        }

        var acceptLanguageHeader = _httpContextAccessor.HttpContext?.Request
            .GetTypedHeaders()
            .AcceptLanguage
            ?.OrderByDescending(x => x.Quality ?? 1.0)
            .FirstOrDefault();

        if (acceptLanguageHeader != null)
        {
            var languageCode = acceptLanguageHeader.Value.Value?
                .Split('-')[0]
                .Trim();

            if (!string.IsNullOrWhiteSpace(languageCode))
                return LanguageCode.FromCode(languageCode);
        }

        return LanguageCode.FromCode(LanguageCode.Default);
    }
}
