using ELearning.Application.Features.Lessons.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Lessons.Queries.GetLessonById;

public sealed class GetLessonByIdQueryHandler
    : IRequestHandler<GetLessonByIdQuery, LessonDto?>
{
    private readonly IApplicationDbContext _context;

    public GetLessonByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LessonDto?> Handle(
        GetLessonByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Lessons
            .AsNoTracking()
            .Where(lesson => lesson.Id == request.Id)
            .Select(lesson => new LessonDto(
                lesson.Id,
                lesson.SectionId,
                lesson.Title,
                lesson.Description,
                lesson.Type,
                lesson.VideoUrl,
                lesson.Content,
                lesson.DurationInSeconds,
                lesson.DisplayOrder,
                lesson.IsFreePreview,
                lesson.IsPublished,
                lesson.CreatedAt,
                lesson.UpdatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}