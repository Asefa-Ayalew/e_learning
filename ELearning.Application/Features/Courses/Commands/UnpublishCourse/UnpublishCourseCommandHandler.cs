using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Courses.Commands.UnpublishCourse;

public sealed class UnpublishCourseCommandHandler
    : IRequestHandler<UnpublishCourseCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UnpublishCourseCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UnpublishCourseCommand request,
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

        if (!course.IsPublished)
        {
            throw new InvalidOperationException(
                "The course is already unpublished.");
        }

        course.IsPublished = false;
        course.PublishedAt = null;
        course.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}