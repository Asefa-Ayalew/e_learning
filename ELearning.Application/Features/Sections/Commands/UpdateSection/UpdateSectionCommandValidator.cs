using FluentValidation;

namespace ELearning.Application.Features.Sections.Commands.UpdateSection;

public sealed class UpdateSectionCommandValidator
: AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {
        RuleFor(section => section.Id)
            .NotEmpty()
            .WithMessage("Section id is required.");

        RuleFor(section => section.Title)
            .NotEmpty()
            .WithMessage("Section title is required.")
            .MaximumLength(200)
            .WithMessage("Section title must not exceed 200 characters.");

        RuleFor(section => section.Description)
            .MaximumLength(2000)
            .WithMessage("Section description must not exceed 2000 characters.");

        RuleFor(section => section.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Display order must be a non-negative integer.");
    }
}