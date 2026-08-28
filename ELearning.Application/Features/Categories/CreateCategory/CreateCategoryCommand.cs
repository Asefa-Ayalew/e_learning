using ELearning.Application.Features.Categories.Common;
using MediatR;

namespace ELearning.Application.Features.Categories.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl)
    : IRequest<CategoryResponse>;