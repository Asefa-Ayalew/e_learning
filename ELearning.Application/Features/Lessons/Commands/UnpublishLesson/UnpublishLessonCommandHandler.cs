using ELearning.Application.Features.Lessons.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Lessons.Commands.UnpublishLesson;

public sealed class UnpublishLessonCommandHandler : IRequestHandler<UnpublishLessonCommand, LessonDto>
{
    private readonly IApplicationDbContext _context;
    public UnpublishLessonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<LessonDto> Handle(
        UnpublishLessonCommand request,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
        .FirstOrDefaultAsync(lesson => lesson.Id == request.Id, cancellationToken);

        if (lesson is null)
        {
            throw new KeyNotFoundException(
                $"Lesson with id '{request.Id}' was not found.");
        }
        lesson.IsPublished = false;
        lesson.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return new LessonDto(
            lesson.Id,
            lesson.SectionId,
            lesson.Title,
            lesson.Description,
            lesson.Type,
            lesson.VideoUrl,
            lesson.Content,
            lesson.DurationInSeconds,
            lesson.DisplayOrder,
            lesson.IsFreePreview,
            lesson.IsPublished,
            lesson.CreatedAt,
            lesson.UpdatedAt
        );
    }
}