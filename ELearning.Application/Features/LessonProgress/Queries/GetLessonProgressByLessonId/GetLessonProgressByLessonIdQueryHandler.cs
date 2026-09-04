using ELearning.Application.Features.LessonProgress.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressByLessonId;

public sealed class GetLessonProgressByLessonIdQueryHandler
    : IRequestHandler<
        GetLessonProgressByLessonIdQuery,
        LessonProgressDto?>
{
    private readonly IApplicationDbContext _context;

    public GetLessonProgressByLessonIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LessonProgressDto?> Handle(
        GetLessonProgressByLessonIdQuery request,
        CancellationToken cancellationToken)
    {
        var progress = await _context.LessonProgresses
            .AsNoTracking()
            .FirstOrDefaultAsync(
                progress =>
                    progress.UserId == request.UserId &&
                    progress.LessonId == request.LessonId,
                cancellationToken);

        if (progress is null)
        {
            return null;
        }

        return new LessonProgressDto(
            progress.Id,
            progress.UserId,
            progress.LessonId,
            progress.IsCompleted,
            progress.WatchedSeconds,
            progress.CompletedAt,
            progress.LastAccessedAt
        );
    }
}