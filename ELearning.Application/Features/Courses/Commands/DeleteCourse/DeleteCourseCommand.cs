using MediatR;

namespace ELearning.Application.Features.Courses.Commands.DeleteCourse;

public sealed record DeleteCourseCommand(
    Guid Id
) : IRequest<bool>;