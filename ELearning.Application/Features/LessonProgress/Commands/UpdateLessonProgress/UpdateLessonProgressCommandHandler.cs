using ELearning.Application.Features.LessonProgress.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.LessonProgress.Commands.UpdateLessonProgress;

public sealed class UpdateLessonProgressCommandHandler
    : IRequestHandler<UpdateLessonProgressCommand, LessonProgressDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateLessonProgressCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LessonProgressDto> Handle(
        UpdateLessonProgressCommand request,
        CancellationToken cancellationToken)
    {
        var progress = await _context.LessonProgresses
            .FirstOrDefaultAsync(
                progress => progress.Id == request.Id,
                cancellationToken);

        if (progress is null)
        {
            throw new KeyNotFoundException(
                $"Lesson progress with ID '{request.Id}' was not found.");
        }

        progress.WatchedSeconds = request.WatchedSeconds;
        progress.LastAccessedAt = DateTime.UtcNow;

        if (request.IsCompleted && !progress.IsCompleted)
        {
            progress.IsCompleted = true;
            progress.CompletedAt = DateTime.UtcNow;
        }
        else if (!request.IsCompleted && progress.IsCompleted)
        {
            progress.IsCompleted = false;
            progress.CompletedAt = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

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