using ELearning.Application.Features.Sections.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Sections.Queries.GetSectionById;

public sealed class GetSectionByIdQueryHandler
    : IRequestHandler<GetSectionByIdQuery, SectionDto?>
{
    private readonly IApplicationDbContext _context;
    public GetSectionByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SectionDto?> Handle(
        GetSectionByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Sections
        .AsNoTracking()
        .Where(section => section.Id == request.Id)
        .Select(section => new SectionDto(
            section.Id,
            section.CourseId,
            section.Title,
            section.Description,
            section.DisplayOrder,
            section.CreatedAt,
            section.UpdatedAt
        ))
        .FirstOrDefaultAsync(cancellationToken);
    }
}