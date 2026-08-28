using FluentValidation;

namespace ELearning.Application.Features.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseCommandValidator
    : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

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

        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x)
            .Must(x => x.IsFree || x.Price > 0)
            .WithMessage(
                "A paid course must have a price greater than zero.");
    }
}