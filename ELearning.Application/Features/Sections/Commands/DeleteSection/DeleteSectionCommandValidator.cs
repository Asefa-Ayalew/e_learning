using FluentValidation;

namespace ELearning.Application.Features.Sections.Commands.DeleteSection;

public sealed class DeleteSectionCommandValidator
: AbstractValidator<DeleteSectionCommand>
{
    public DeleteSectionCommandValidator()
    {
        RuleFor(section => section.Id)
            .NotEmpty()
            .WithMessage("Section id is required.");
    }
}