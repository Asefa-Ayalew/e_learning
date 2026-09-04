using FluentValidation;

namespace ELearning.Application.Features.LessonProgress.Commands.DeleteLessonProgress;

public sealed class DeleteLessonProgressCommandValidator
: AbstractValidator<DeleteLessonProgressCommand>
{
    public DeleteLessonProgressCommandValidator()
    {
        RuleFor(progress => progress.Id)
        .NotEmpty()
        .WithMessage("Id must not be empty");
    }
}