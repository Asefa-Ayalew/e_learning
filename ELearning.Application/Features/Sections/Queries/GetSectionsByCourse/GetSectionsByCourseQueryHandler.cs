using ELearning.Application.Features.Sections.Common;
using ELearning.Application.Features.Sections.Queries.GetSEctionsByCourse;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Sections.Queries.GetSectionsByCourse;

public sealed class GetSectionsByCourseQueryHandler
    : IRequestHandler<GetSectionsByCourseQuery, IReadOnlyList<SectionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSectionsByCourseQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<SectionDto>> Handle(
        GetSectionsByCourseQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Sections
            .AsNoTracking()
            .Where(section => section.CourseId == request.CourseId)
            .OrderBy(section => section.DisplayOrder)
            .Select(section => new SectionDto(
                section.Id,
                section.CourseId,
                section.Title,
                section.Description,
                section.DisplayOrder,
                section.CreatedAt,
                section.UpdatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}