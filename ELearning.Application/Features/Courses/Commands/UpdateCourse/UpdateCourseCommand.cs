using ELearning.Application.Features.Courses.Common;
using MediatR;

namespace ELearning.Application.Features.Courses.Commands.UpdateCourse;

public sealed record UpdateCourseCommand(
    Guid Id,
    string Title,
    string Slug,
    string? ShortDescription,
    string? Description,
    string? ThumbnailUrl,
    decimal Price,
    bool IsFree,
    Guid InstructorId,
    Guid CategoryId
) : IRequest<CourseResponseDto?>;