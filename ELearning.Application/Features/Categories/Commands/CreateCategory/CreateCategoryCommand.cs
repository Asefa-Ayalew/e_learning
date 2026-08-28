using ELearning.Application.Features.Categories.Common;
using MediatR;

namespace ELearning.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl)
    : IRequest<CategoryResponse>;