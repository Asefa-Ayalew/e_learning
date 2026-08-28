using ELearning.Application.Features.Categories.Common;
using MediatR;

namespace ELearning.Application.Features.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(
    Guid Id
) : IRequest<CategoryResponse>;