using FluentValidation;

namespace ELearning.Application.Features.Lessons.Commands.PublishLesson;

public sealed class PublishLessonCommandValidator
: AbstractValidator<PublishLessonCommand>
{
    public PublishLessonCommandValidator()
    {
        RuleFor(lesson => lesson.Id)
            .NotEmpty().WithMessage("Lesson Id is required.");
    }
}