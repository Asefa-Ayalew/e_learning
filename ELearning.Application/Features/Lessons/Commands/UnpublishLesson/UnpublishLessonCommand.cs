using ELearning.Application.Features.Lessons.Common;
using MediatR;

namespace ELearning.Application.Features.Lessons.Commands.UnpublishLesson;

public sealed record UnpublishLessonCommand(
    Guid Id
) : IRequest<LessonDto>;