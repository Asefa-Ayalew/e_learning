using ELearning.Application.Features.LessonProgress.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.LessonProgress.Queries.GetLessonProgressById;

public sealed class GetLessonProgressByIdQueryHandler
    : IRequestHandler<GetLessonProgressByIdQuery, LessonProgressDto>
{
    private readonly IApplicationDbContext _context;

    public GetLessonProgressByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LessonProgressDto> Handle(
        GetLessonProgressByIdQuery request,
        CancellationToken cancellationToken)
    {
        var progress = await _context.LessonProgresses
            .AsNoTracking()
            .FirstOrDefaultAsync(
                progress => progress.Id == request.Id,
                cancellationToken);

        if (progress is null)
        {
            throw new KeyNotFoundException(
                $"Lesson progress with ID '{request.Id}' was not found.");
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