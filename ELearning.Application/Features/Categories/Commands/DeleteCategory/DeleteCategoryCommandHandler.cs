using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Categories.Commands.DeleteCategory;

public sealed class DeleteCategoryCommandHandler
    : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteCategoryCommandHandler(
        IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(
                category => category.Id == request.Id,
                cancellationToken);

        if (category is null)
        {
            return false;
        }

        _dbContext.Categories.Remove(category);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}