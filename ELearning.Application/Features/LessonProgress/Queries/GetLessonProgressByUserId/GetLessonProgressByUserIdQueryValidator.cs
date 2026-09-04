using FluentValidation;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByUserId;

public sealed class GetLessonProgressByUserIdQueryValidator
: AbstractValidator<GetLessonProgressByUserIdQuery>
{
    public GetLessonProgressByUserIdQueryValidator()
    {
        RuleFor(progress => progress.UserId)
        .NotEmpty()
        .WithMessage("User Id is required");
    }
}