using FluentValidation;
using StepITFinalProject.Requests;
namespace StepITFinalProject.Validators;

public class BorrowRequestValidator : AbstractValidator<BorrowRequest>
{
    public BorrowRequestValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0).WithMessage("Book id is required.");
    }
}