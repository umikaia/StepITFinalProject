using FluentValidation;
using StepITFinalProject.Requests;

namespace StepITFinalProject.Validators;

public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorRequest>
{
    public CreateAuthorRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(1, 60).WithMessage("First name must be between 1 and 60 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(1, 60).WithMessage("Last name must be between 1 and 60 characters.");

        RuleFor(x => x.BirthYear)
            .InclusiveBetween(-2500, DateTime.UtcNow.Year)
            .WithMessage($"Birth year must be between -2500 and {DateTime.UtcNow.Year}.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .Length(2, 80).WithMessage("Country must be between 2 and 80 characters.");
    }
}
