using ELearning.Application.Features.LessonProgress.Common;
using MediatR;

namespace ELearning.Application.Features.LessonProgress.Commands.CreateLessonProgress;

public sealed record CreateLessonProgressCommand(
    Guid UserId,
    Guid LessonId,
    int WatchedSeconds
) : IRequest<LessonProgressDto>;