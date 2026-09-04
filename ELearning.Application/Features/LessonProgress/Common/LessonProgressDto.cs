namespace ELearning.Application.Features.LessonProgress.Common;

public sealed record LessonProgressDto(
    Guid Id,
    Guid UserId,
    Guid LessonId,
    bool IsCompleted,
    int WatchedSeconds,
    DateTime? CompletedAt,
    DateTime LastAccessedAt
);