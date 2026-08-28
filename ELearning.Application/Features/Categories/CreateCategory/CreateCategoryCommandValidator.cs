using FluentValidation;

namespace ELearning.Application.Features.Categories.CreateCategory;

public sealed class CreateCategoryCommandValidator
    : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
            .WithMessage(
                "Slug must contain lowercase letters, numbers, and hyphens only.");

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.ImageUrl)
            .MaximumLength(1000);
    }
}