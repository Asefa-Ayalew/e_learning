using ELearning.Application.Features.Courses.Common;
using MediatR;

namespace ELearning.Application.Features.Courses.Queries.GetCourseById;

public sealed record GetCourseByIdQuery(
    Guid Id
) : IRequest<CourseResponseDto?>;