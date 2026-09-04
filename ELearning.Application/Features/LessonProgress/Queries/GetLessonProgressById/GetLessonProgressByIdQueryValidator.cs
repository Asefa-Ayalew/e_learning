using FluentValidation;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressById;

public sealed class GetLessonProgressByIdQueryValidator
: AbstractValidator<GetLessonProgressByIdQuery>
{
    public GetLessonProgressByIdQueryValidator()
    {
        RuleFor(progress => progress.Id)
        .NotEmpty()
        .WithMessage("Id is required");
    }
}