using FluentValidation;

namespace ELearning.Application.Features.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandValidator
    : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200)
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
            .WithMessage(
                "Slug must contain lowercase letters, numbers, and hyphens only.");

        RuleFor(x => x.ShortDescription)
            .MaximumLength(500);

        RuleFor(x => x.Description)
            .MaximumLength(5000);

        RuleFor(x => x.ThumbnailUrl)
            .MaximumLength(1000);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.InstructorId)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x)
            .Must(x => x.IsFree || x.Price > 0)
            .WithMessage(
                "A paid course must have a price greater than zero.");
    }
}