using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.LessonProgress.Commands.DeleteLessonProgress;

public sealed class DeleteLessonProgressCommandHandler
: IRequestHandler<DeleteLessonProgressCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public DeleteLessonProgressCommandHandler(
        IApplicationDbContext context
    )
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteLessonProgressCommand request,
        CancellationToken cancellationToken
    )
    {
        var lessonProgress = await _context.LessonProgresses
        .FirstOrDefaultAsync(progress => progress.Id == request.Id, cancellationToken);

        if (lessonProgress is null)
        {
            throw new KeyNotFoundException(
                $"Lesson progress with ID '{request.Id}' not found"
            );
        }
        _context.LessonProgresses.Remove(lessonProgress);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}