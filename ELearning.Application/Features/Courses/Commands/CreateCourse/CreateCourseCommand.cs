using ELearning.Application.Features.Courses.Common;
using MediatR;

namespace ELearning.Application.Features.Courses.Commands.CreateCourse;

public sealed record CreateCourseCommand(
    string Title,
    string Slug,
    string? ShortDescription,
    string? Description,
    string? ThumbnailUrl,
    decimal Price,
    bool IsFree,
    Guid InstructorId,
    Guid CategoryId
) : IRequest<CourseResponseDto>;