using ELearning.Application.Features.Categories.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler
    : IRequestHandler<UpdateCategoryCommand, CategoryResponse?>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateCategoryCommandHandler(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryResponse?> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(
                category => category.Id == request.Id,
                cancellationToken);

        if (category is null)
        {
            return null;
        }

        var name = request.Name.Trim();
        var slug = request.Slug.Trim().ToLowerInvariant();

        var nameExists = await _dbContext.Categories
            .AnyAsync(
                category =>
                    category.Id != request.Id &&
                    category.Name.ToLower() == name.ToLower(),
                cancellationToken);
        Console.WriteLine(category);
        Console.WriteLine(request);

        if (nameExists)
        {
            throw new InvalidOperationException(
                "A category with this name already exists.");
        }

        var slugExists = await _dbContext.Categories
            .AnyAsync(
                category =>
                    category.Id != request.Id &&
                    category.Slug == slug,
                cancellationToken);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "A category with this slug already exists.");
        }

        category.Name = name;
        category.Slug = slug;
        category.Description = request.Description?.Trim();
        category.ImageUrl = request.ImageUrl?.Trim();
        category.IsActive = request.IsActive;
        category.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CategoryResponse(
            category.Id,
            category.Name,
            category.Slug,
            category.Description,
            category.ImageUrl,
            category.IsActive,
            category.CreatedAt,
            category.UpdatedAt);
    }
}