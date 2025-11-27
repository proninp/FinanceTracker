using FinanceTracker.App.Accounts.Application.Contracts.DTOs.Accounts;
using FluentValidation;

namespace FinanceTracker.App.Accounts.Application.Validators;

public sealed class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(x => x.AccountTypeId)
            .NotEmpty()
            .WithMessage("AccountTypeId is required.");

        RuleFor(x => x.CurrencyId)
            .NotEmpty()
            .WithMessage("CurrencyId is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(200)
            .WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.CreditLimit)
            .GreaterThanOrEqualTo(0)
            .When(x => x.CreditLimit.HasValue)
            .WithMessage("CreditLimit must be non-negative.");
    }
}
