using FluentValidation;

namespace ELearning.Application.Features.Lessons.Commands.CreateLesson;

public sealed class CreateLessonCommandValidator
    : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(lesson => lesson.SectionId)
            .NotEmpty()
            .WithMessage("Section id is required.");

        RuleFor(lesson => lesson.Title)
            .NotEmpty()
            .WithMessage("Lesson title is required.")
            .MaximumLength(200)
            .WithMessage("Lesson title must not exceed 200 characters.");

        RuleFor(lesson => lesson.Description)
            .MaximumLength(2000)
            .WithMessage("Lesson description must not exceed 2000 characters.")
            .When(lesson => lesson.Description is not null);

        RuleFor(lesson => lesson.Type)
            .IsInEnum()
            .WithMessage("Invalid lesson type.");

        RuleFor(lesson => lesson.VideoUrl)
            .MaximumLength(1000)
            .WithMessage("Video URL must not exceed 1000 characters.")
            .When(lesson => lesson.VideoUrl is not null);

        RuleFor(lesson => lesson.Content)
            .MaximumLength(100000)
            .WithMessage("Lesson content is too long.")
            .When(lesson => lesson.Content is not null);

        RuleFor(lesson => lesson.DurationInSeconds)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Duration must be zero or greater.");

        RuleFor(lesson => lesson.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Display order must be zero or greater.");
    }
}