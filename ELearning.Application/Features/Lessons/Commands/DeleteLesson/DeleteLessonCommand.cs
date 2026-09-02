using MediatR;

namespace ELearning.Application.Features.Lessons.Commands.DeleteLesson;

public sealed record DeleteLessonCommand(Guid Id) : IRequest;