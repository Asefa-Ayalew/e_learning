using ELearning.Application.Features.Courses.Common;
using ELearning.Application.Interfaces;
using ELearning.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandler
    : IRequestHandler<CreateCourseCommand, CourseResponseDto>
{
    private readonly IApplicationDbContext _context;

    public CreateCourseCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseResponseDto> Handle(
        CreateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var title = request.Title.Trim();
        var slug = request.Slug.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Course title is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new ArgumentException(
                "Course slug is required.");
        }

        if (request.Price < 0)
        {
            throw new ArgumentException(
                "Course price cannot be negative.");
        }

        if (!request.IsFree && request.Price <= 0)
        {
            throw new ArgumentException(
                "A paid course must have a price greater than zero.");
        }
        Console.WriteLine(_context.Users);
        Console.WriteLine(request.InstructorId);
        var instructorExists = await _context.Users
            .AnyAsync(
                user => user.Id == request.InstructorId,
                cancellationToken);

        if (!instructorExists)
        {
            throw new KeyNotFoundException(
                "Instructor was not found.");
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

        var slugExists = await _context.Courses
            .AnyAsync(
                course => course.Slug == slug,
                cancellationToken);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "A course with this slug already exists.");
        }

        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = title,
            Slug = slug,
            ShortDescription = request.ShortDescription?.Trim(),
            Description = request.Description?.Trim(),
            ThumbnailUrl = request.ThumbnailUrl?.Trim(),
            Price = request.IsFree ? 0 : request.Price,
            IsFree = request.IsFree,
            IsPublished = false,
            PublishedAt = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            InstructorId = request.InstructorId,
            CategoryId = request.CategoryId
        };

        _context.Courses.Add(course);

        await _context.SaveChangesAsync(cancellationToken);

        return await _context.Courses
     .AsNoTracking()
     .Where(c => c.Id == course.Id)
     .Select(c => new CourseResponseDto(
         c.Id,
         c.Title,
         c.Slug,
         c.ShortDescription,
         c.Description,
         c.ThumbnailUrl,
         c.Price,
         c.IsFree,
         c.IsPublished,
         c.PublishedAt,
         c.CreatedAt,
         c.UpdatedAt,
         c.InstructorId,
         c.Instructor.FirstName + " " +
         c.Instructor.LastName,
         c.CategoryId,
         c.Category.Name))
     .FirstAsync(cancellationToken);
    }
}