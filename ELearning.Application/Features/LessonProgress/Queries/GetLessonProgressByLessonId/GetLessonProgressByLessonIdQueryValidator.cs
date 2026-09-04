using FluentValidation;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByLessonId;

public sealed class GetLessonProgressByLessonIdQueryValidator
: AbstractValidator<GetLessonProgressByLessonIdQuery>
{
    public GetLessonProgressByLessonIdQueryValidator()
    {
        RuleFor(progress => progress.UserId)
        .NotEmpty()
        .WithMessage("UserId is required");

        RuleFor(progress => progress.LessonId)
        .NotEmpty()
        .WithMessage("Lesson Id is required");
    }
}