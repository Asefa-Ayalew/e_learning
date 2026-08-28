using ELearning.Application.Features.Categories.Common;
using MediatR;

namespace ELearning.Application.Features.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl,
    bool IsActive)
    : IRequest<CategoryResponse>;