using ELearning.Application.Features.Sections.Queries.GetSEctionsByCourse;
using FluentValidation;

namespace ELearning.Application.Features.Sections.Queries.GetSectionsByCourse;

public sealed class GetSectionsByCourseQueryValidator
: AbstractValidator<GetSectionsByCourseQuery>
{
    public GetSectionsByCourseQueryValidator()
    {
        RuleFor(section => section.CourseId)
            .NotEmpty()
            .WithMessage("Course id is required.");
    }
}