using FluentValidation;
using StepITFinalProject.Requests;
namespace StepITFinalProject.Validators;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(1, 200).WithMessage("Title must be between 1 and 200 characters.");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^(\d{10}|\d{13})$").WithMessage("ISBN must contain 10 or 13 digits.");

        RuleFor(x => x.PublicationYear)
            .InclusiveBetween(1450, DateTime.UtcNow.Year)
            .WithMessage($"Published year must be between 1450 and {DateTime.UtcNow.Year}.");

        RuleFor(x => x.TotalCopies)
            .InclusiveBetween(1, 1000).WithMessage("Total copies must be between 1 and 1000.");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("Author id is required.");

        RuleFor(x => x.GenreIds)
            .NotEmpty().WithMessage("At least one genre is required.")
            .Must(ids => ids.Distinct().Count() == ids.Count).WithMessage("Duplicate genres are not allowed.");

        RuleForEach(x => x.GenreIds)
            .GreaterThan(0).WithMessage("Genre id must be a positive number.");
    }

}
