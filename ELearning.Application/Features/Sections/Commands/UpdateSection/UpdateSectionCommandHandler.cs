using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Sections.Commands.UpdateSection;

public sealed class UpdateSectionCommandHandler
    : IRequestHandler<UpdateSectionCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateSectionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UpdateSectionCommand request,
        CancellationToken cancellationToken)
    {
        var section = await _context.Sections
            .FirstOrDefaultAsync(
                section => section.Id == request.Id,
                cancellationToken);

        if (section is null)
        {
            return false;
        }

        section.Title = request.Title;
        section.Description = request.Description;
        section.DisplayOrder = request.DisplayOrder;
        section.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}