using ELearning.Application.Features.Courses.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Courses.Queries.GetCourseById;

public sealed class GetCourseByIdQueryHandler
    : IRequestHandler<GetCourseByIdQuery, CourseResponseDto?>
{
    private readonly IApplicationDbContext _context;

    public GetCourseByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseResponseDto?> Handle(
        GetCourseByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Courses
            .AsNoTracking()
            .Where(course => course.Id == request.Id)
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
                course.Category.Name))
            .FirstOrDefaultAsync(cancellationToken);
    }
}