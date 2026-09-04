using ELearning.Application.Features.LessonProgress.Common;
using MediatR;

namespace ELearning.Application.Features.LessonProgress.Commands.UpdateLessonProgress;

public sealed record UpdateLessonProgressCommand(
    Guid Id,
    int WatchedSeconds,
    bool IsCompleted
) : IRequest<LessonProgressDto>;