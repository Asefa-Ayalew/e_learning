using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Sections.Commands.DeleteSection;

public sealed class DeleteSectionCommandHandler
    : IRequestHandler<DeleteSectionCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteSectionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        DeleteSectionCommand request,
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

        _context.Sections.Remove(section);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}