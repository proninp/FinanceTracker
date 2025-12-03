using FinanceTracker.App.ShareKernel.Domain.Localization;

namespace FinanceTracker.App.ShareKernel.Application.Localization;

public interface ILanguageContext
{
    Language CurrentLanguage { get; }

    string CurrentLanguageCode { get; }
}
