using ELearning.Application.Common.Models;
using ELearning.Application.Features.Lessons.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Lessons.Queries.GetLessons;

public sealed class GetLessonsQueryHandler
    : IRequestHandler<GetLessonsQuery, PagedResult<LessonDto>>
{
    private readonly IApplicationDbContext _context;

    public GetLessonsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<LessonDto>> Handle(
        GetLessonsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Lessons
            .AsNoTracking()
            .AsQueryable();

        if (request.SectionId.HasValue)
        {
            query = query.Where(
                lesson => lesson.SectionId == request.SectionId.Value);
        }


        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            query = query.Where(
                lesson => EF.Functions.ILike(
                    lesson.Title,
                    $"%{search}%"));
        }

        var totalCount = await query.CountAsync(
            cancellationToken);

        var page = request.Page < 1
            ? 1
            : request.Page;

        var pageSize = request.PageSize < 1
            ? 20
            : Math.Min(request.PageSize, 100);

        var lessons = await query
            .OrderBy(lesson => lesson.DisplayOrder)
            .ThenBy(lesson => lesson.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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
            .ToListAsync(cancellationToken);

        return new PagedResult<LessonDto>
        {
            Items = lessons,
            Total = totalCount
        };
    }
}