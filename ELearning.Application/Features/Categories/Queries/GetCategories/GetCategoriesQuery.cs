using ELearning.Application.Common.Models;
using ELearning.Application.Features.Categories.Common;
using MediatR;

namespace ELearning.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery
(CollectionQuery Query) : IRequest<PagedResult<CategoryResponse>>;