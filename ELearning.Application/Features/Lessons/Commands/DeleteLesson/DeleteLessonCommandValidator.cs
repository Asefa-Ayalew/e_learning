using FluentValidation;

namespace ELearning.Application.Features.Lessons.Commands.DeleteLesson;

public sealed class DeleteLessonCommandValidator : AbstractValidator<DeleteLessonCommand>
{
    public DeleteLessonCommandValidator()
    {
        RuleFor(lesson => lesson.Id)
            .NotEmpty().WithMessage("Lesson Id is required.");
    }
}