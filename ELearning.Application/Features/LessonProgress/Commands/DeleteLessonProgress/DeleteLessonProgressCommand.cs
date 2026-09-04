using MediatR;

namespace ELearning.Application.Features.LessonProgress.Commands.DeleteLessonProgress;

public sealed record DeleteLessonProgressCommand(
    Guid Id
) : IRequest<Unit>;