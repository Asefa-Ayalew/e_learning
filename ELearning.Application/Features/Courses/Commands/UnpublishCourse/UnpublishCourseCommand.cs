using MediatR;

namespace ELearning.Application.Features.Courses.Commands.UnpublishCourse;

public sealed record UnpublishCourseCommand(
    Guid Id
) : IRequest<bool>;