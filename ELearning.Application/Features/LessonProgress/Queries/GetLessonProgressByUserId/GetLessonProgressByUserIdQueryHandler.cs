using ELearning.Application.Features.LessonProgress.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByUserId;

public sealed class GetLessonProgressByUserIdQueryHandler
    : IRequestHandler<
        GetLessonProgressByUserIdQuery,
        IReadOnlyList<LessonProgressDto>>
{
    private readonly IApplicationDbContext _context;

    public GetLessonProgressByUserIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<LessonProgressDto>> Handle(
        GetLessonProgressByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.LessonProgresses
            .AsNoTracking()
            .Where(progress => progress.UserId == request.UserId)
            .OrderByDescending(progress => progress.LastAccessedAt)
            .Select(progress => new LessonProgressDto(
                progress.Id,
                progress.UserId,
                progress.LessonId,
                progress.IsCompleted,
                progress.WatchedSeconds,
                progress.CompletedAt,
                progress.LastAccessedAt
            ))
            .ToListAsync(cancellationToken);
    }
}