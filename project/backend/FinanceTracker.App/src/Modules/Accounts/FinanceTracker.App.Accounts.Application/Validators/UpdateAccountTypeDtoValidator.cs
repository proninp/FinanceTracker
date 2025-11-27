using FinanceTracker.App.Accounts.Application.Contracts.DTOs.AccountTypes;
using FluentValidation;

namespace FinanceTracker.App.Accounts.Application.Validators;

public sealed class UpdateAccountTypeDtoValidator : AbstractValidator<UpdateAccountTypeDto>
{
    public UpdateAccountTypeDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code is required.")
            .MaximumLength(50)
            .WithMessage("Code must not exceed 50 characters.")
            .Matches("^[A-Z0-9_]+$")
            .WithMessage("Code must contain only uppercase letters, numbers, and underscores.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");
    }
}
