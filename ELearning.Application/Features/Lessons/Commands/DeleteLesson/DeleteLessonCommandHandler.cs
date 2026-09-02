using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Lessons.Commands.DeleteLesson;

public sealed class DeleteLessonCommandHandler
    : IRequestHandler<DeleteLessonCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLessonCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteLessonCommand request,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(
                lesson => lesson.Id == request.Id,
                cancellationToken);

        if (lesson is null)
        {
            throw new KeyNotFoundException(
                $"Lesson with id '{request.Id}' was not found.");
        }

        _context.Lessons.Remove(lesson);

        await _context.SaveChangesAsync(cancellationToken);
    }
}