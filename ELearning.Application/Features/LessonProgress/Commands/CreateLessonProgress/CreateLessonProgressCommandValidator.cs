using FluentValidation;

namespace ELearning.Application.Features.LessonProgress.Commands.CreateLessonProgress;

public sealed class CreateLessonProgressCommandValidator
: AbstractValidator<CreateLessonProgressCommand>
{
    public CreateLessonProgressCommandValidator()
    {
        RuleFor(progress => progress.UserId)
        .NotEmpty()
        .WithMessage("User Id must not be empty");

        RuleFor(progress => progress.LessonId)
        .NotEmpty()
        .WithMessage("Lesson Id must not be empty");

        RuleFor(progress => progress.WatchedSeconds)
        .GreaterThanOrEqualTo(0)
        .WithMessage("Watched seconds cannot be negative");
    }
}