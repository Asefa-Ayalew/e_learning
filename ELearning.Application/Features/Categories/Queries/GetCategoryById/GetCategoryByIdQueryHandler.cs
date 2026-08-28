using ELearning.Application.Features.Categories.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler
    : IRequestHandler<GetCategoryByIdQuery, CategoryResponse?>
{
    private readonly IApplicationDbContext _dbContext;

    public GetCategoryByIdQueryHandler(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryResponse?> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .Where(category => category.Id == request.Id)
            .Select(category => new CategoryResponse(
                category.Id,
                category.Name,
                category.Slug,
                category.Description,
                category.ImageUrl,
                category.IsActive,
                category.CreatedAt,
                category.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}