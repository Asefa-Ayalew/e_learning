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

        if (!string.IsNullOrWhiteSpace(request.Query.Search))
        {
            var search = request.Query.Search.Trim();

            query = query.Where(course =>
                EF.Functions.ILike(
                    course.Title,
                    $"%{search}%") ||

                EF.Functions.ILike(
                    course.Slug,
                    $"%{search}%") ||

                (course.ShortDescription != null &&
                 EF.Functions.ILike(
                     course.ShortDescription,
                     $"%{search}%")));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(course =>
                course.CategoryId == request.CategoryId.Value);
        }

        if (request.IsPublished.HasValue)
        {
            query = query.Where(course =>
                course.IsPublished == request.IsPublished.Value);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(course =>
                course.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(course =>
                course.Price <= request.MaxPrice.Value);
        }

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

        return await projectedQuery.ToPagedResultAsync(
            request.Query,
            cancellationToken);
    }
}