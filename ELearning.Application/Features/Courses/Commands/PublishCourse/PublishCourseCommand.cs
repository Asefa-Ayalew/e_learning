using MediatR;

namespace ELearning.Application.Features.Courses.Commands.PublishCourse;

public sealed record PublishCourseCommand(
    Guid Id
) : IRequest<bool>;