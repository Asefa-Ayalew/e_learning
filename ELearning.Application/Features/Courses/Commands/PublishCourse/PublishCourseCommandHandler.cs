using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Courses.Commands.PublishCourse;

public sealed class PublishCourseCommandHandler
    : IRequestHandler<PublishCourseCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public PublishCourseCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        PublishCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(
                course => course.Id == request.Id,
                cancellationToken);

        if (course is null)
        {
            return false;
        }

        if (course.IsPublished)
        {
            throw new InvalidOperationException(
                "The course is already published.");
        }

        course.IsPublished = true;
        course.PublishedAt = DateTime.UtcNow;
        course.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}