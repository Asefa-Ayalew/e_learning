using ELearning.Application.Features.Courses.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseCommandHandler
    : IRequestHandler<UpdateCourseCommand, CourseResponseDto?>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseResponseDto?> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(
                course => course.Id == request.Id,
                cancellationToken);

        if (course is null)
        {
            return null;
        }

        var title = request.Title.Trim();
        var slug = request.Slug.Trim().ToLowerInvariant();

        var slugExists = await _context.Courses
            .AnyAsync(
                existingCourse =>
                    existingCourse.Id != request.Id &&
                    existingCourse.Slug == slug,
                cancellationToken);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "A course with this slug already exists.");
        }

        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(
                category => category.Id == request.CategoryId,
                cancellationToken);

        if (category is null)
        {
            throw new KeyNotFoundException(
                "Category was not found.");
        }

        if (!category.IsActive)
        {
            throw new InvalidOperationException(
                "The selected category is inactive.");
        }

        course.Title = title;
        course.Slug = slug;
        course.ShortDescription =
            request.ShortDescription?.Trim();
        course.Description =
            request.Description?.Trim();
        course.ThumbnailUrl =
            request.ThumbnailUrl?.Trim();

        course.Price = request.IsFree
            ? 0
            : request.Price;

        course.IsFree = request.IsFree;
        course.CategoryId = request.CategoryId;
        course.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(
            cancellationToken);

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
            .FirstAsync(cancellationToken);
    }
}