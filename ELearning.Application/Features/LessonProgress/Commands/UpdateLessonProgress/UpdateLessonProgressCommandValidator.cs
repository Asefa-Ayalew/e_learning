using FluentValidation;

namespace ELearning.Application.Features.LessonProgress.Commands.UpdateLessonProgress;

public sealed class UpdateLessonProgressCommandValidator
: AbstractValidator<UpdateLessonProgressCommand>
{
    public UpdateLessonProgressCommandValidator()
    {
        RuleFor(progress => progress.Id)
        .NotEmpty()
        .WithMessage("Id is Required");

        RuleFor(progress => progress.WatchedSeconds)
        .GreaterThanOrEqualTo(0).
        WithMessage("Watched seconds cannot be negative");
    }
}