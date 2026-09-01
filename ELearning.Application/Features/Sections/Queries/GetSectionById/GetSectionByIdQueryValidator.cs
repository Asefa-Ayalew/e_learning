using FluentValidation;

namespace ELearning.Application.Features.Sections.Queries.GetSectionById;

public sealed class GetSectionByIdQueryValidator
    : AbstractValidator<GetSectionByIdQuery>
{
    public GetSectionByIdQueryValidator()
    {
        RuleFor(section => section.Id)
            .NotEmpty()
            .WithMessage("Section id is required.");
    }
}