using FluentValidation;
using StepITFinalProject.Requests;
namespace StepITFinalProject.Validators;

public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
{
    public CreateGenreRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .Length(2, 60).WithMessage("Genre name must be between 2 and 60 characters.");
    }
}
