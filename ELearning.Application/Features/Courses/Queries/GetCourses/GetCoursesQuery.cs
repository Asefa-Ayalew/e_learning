using ELearning.Application.Common.Models;
using ELearning.Application.Features.Courses.Common;
using MediatR;

namespace ELearning.Application.Features.Courses.Queries.GetCourses;

public sealed record GetCoursesQuery(
    CollectionQuery Query
) : IRequest<PagedResult<CourseResponseDto>>;