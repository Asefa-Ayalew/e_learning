using ELearning.Application.Features.LessonProgress.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.LessonProgress.Commands.CreateLessonProgress;

public sealed class CreateLessonProgressCommandHandler
    : IRequestHandler<CreateLessonProgressCommand, LessonProgressDto>
{
    private readonly IApplicationDbContext _context;

    public CreateLessonProgressCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LessonProgressDto> Handle(
        CreateLessonProgressCommand request,
        CancellationToken cancellationToken)
    {
        var lessonExists = await _context.Lessons
            .AnyAsync(
                lesson => lesson.Id == request.LessonId,
                cancellationToken);

        if (!lessonExists)
        {
            throw new KeyNotFoundException(
                $"Lesson with ID '{request.LessonId}' was not found.");
        }

        // Prevent duplicate progress for the same user and lesson
        var existingProgress = await _context.LessonProgresses
            .AnyAsync(
                progress =>
                    progress.UserId == request.UserId &&
                    progress.LessonId == request.LessonId,
                cancellationToken);

        if (existingProgress)
        {
            throw new InvalidOperationException(
                "Lesson progress already exists for this user and lesson.");
        }

        var progress = new Domain.Entities.LessonProgress
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            LessonId = request.LessonId,
            WatchedSeconds = request.WatchedSeconds,
            IsCompleted = false,
            CompletedAt = null,
            LastAccessedAt = DateTime.UtcNow
        };

        _context.LessonProgresses.Add(progress);

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