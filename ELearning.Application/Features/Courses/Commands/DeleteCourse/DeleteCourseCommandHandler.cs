using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Courses.Commands.DeleteCourse;

public sealed class DeleteCourseCommandHandler
    : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteCourseCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        DeleteCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(
                c => c.Id == request.Id,
                cancellationToken);

        if (course is null)
        {
            return false;
        }

        _context.Courses.Remove(course);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}