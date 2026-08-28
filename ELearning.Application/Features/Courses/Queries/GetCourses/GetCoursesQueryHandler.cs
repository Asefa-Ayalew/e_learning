using ELearning.Application.Common.Extensions;
using ELearning.Application.Common.Models;
using ELearning.Application.Features.Courses.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Courses.Queries.GetCourses;

public sealed class GetCoursesQueryHandler
    : IRequestHandler<GetCoursesQuery, PagedResult<CourseResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCoursesQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<CourseResponseDto>> Handle(
        GetCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Courses
            .AsNoTracking();

        // ---------------------------------------------------------
        // SEARCH
        // ---------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.Query.Search))
        {
            var search = request.Query.Search
                .Trim()
                .ToLower();

            query = query.Where(course =>
                course.Title.ToLower().Contains(search) ||

                course.Slug.ToLower().Contains(search) ||

                (course.ShortDescription != null &&
                 course.ShortDescription
                     .ToLower()
                     .Contains(search)));
        }

        // ---------------------------------------------------------
        // PROJECTION
        // ---------------------------------------------------------

        var projectedQuery = query
            .OrderByDescending(course => course.CreatedAt)
            .Select(course => new CourseResponseDto(
                course.Id,
                course.Title,
                course.Slug,
                course.ShortDescription,
                course.Description,
                course.ThumbnailUrl,
                course.Price,
                course.IsFree,
                course.IsPublished,
                course.PublishedAt,
                course.CreatedAt,
                course.UpdatedAt,
                course.InstructorId,
                course.Instructor.FirstName + " " +
                course.Instructor.LastName,
                course.CategoryId,
                course.Category.Name
            ));

        // ---------------------------------------------------------
        // PAGINATION
        // ---------------------------------------------------------

        return await projectedQuery.ToPagedResultAsync(
            request.Query,
            cancellationToken);
    }
}