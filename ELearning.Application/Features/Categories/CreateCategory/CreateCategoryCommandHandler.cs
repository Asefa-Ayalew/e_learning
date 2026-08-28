using ELearning.Application.Features.Categories.Common;
using ELearning.Application.Interfaces;
using ELearning.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Categories.CreateCategory;

public sealed class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateCategoryCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryResponse> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var name = request.Name.Trim();
        var slug = request.Slug.Trim().ToLowerInvariant();

        var nameExists = await _dbContext.Categories
            .AnyAsync(
                category => category.Name.ToLower() == name.ToLower(),
                cancellationToken);

        if (nameExists)
        {
            throw new InvalidOperationException(
                "A category with this name already exists.");
        }

        var slugExists = await _dbContext.Categories
            .AnyAsync(
                category => category.Slug == slug,
                cancellationToken);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "A category with this slug already exists.");
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),

            Name = name,

            Slug = slug,

            Description =
                request.Description?.Trim(),

            ImageUrl =
                request.ImageUrl?.Trim(),

            IsActive = true,

            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Categories.Add(category);

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