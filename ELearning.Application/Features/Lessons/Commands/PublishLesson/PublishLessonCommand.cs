using ELearning.Application.Features.Lessons.Common;
using MediatR;

namespace ELearning.Application.Features.Lessons.Commands.PublishLesson;

public sealed record PublishLessonCommand(
    Guid Id
) : IRequest<LessonDto>;