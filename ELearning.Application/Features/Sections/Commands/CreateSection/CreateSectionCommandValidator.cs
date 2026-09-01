using FluentValidation;

namespace ELearning.Application.Features.Sections.Commands.CreateSection;

public sealed class CreateSectionCommandValidator
    : AbstractValidator<CreateSectionCommand>
{
    public CreateSectionCommandValidator()
    {
        RuleFor(section => section.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required.");

        RuleFor(section => section.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters.");

        RuleFor(section => section.Description)
            .MaximumLength(5000)
            .WithMessage("Description must not exceed 5000 characters.")
            .When(section => !string.IsNullOrWhiteSpace(section.Description));

        RuleFor(section => section.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("DisplayOrder must be greater than or equal to 0.");
    }
}