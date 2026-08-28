using ELearning.Application.Common.Extensions;
using ELearning.Application.Common.Models;
using ELearning.Application.Features.Categories.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetCategoriesQueryHandler(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<CategoryResponse>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .Select(category => new CategoryResponse(
                category.Id,
                category.Name,
                category.Slug,
                category.Description,
                category.ImageUrl,
                category.IsActive,
                category.CreatedAt,
                category.UpdatedAt));

        return await query.ToPagedResultAsync(
            request.Query,
            cancellationToken);
    }
}