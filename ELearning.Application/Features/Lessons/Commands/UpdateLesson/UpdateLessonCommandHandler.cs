using ELearning.Application.Features.Lessons.Common;
using ELearning.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Lessons.Commands.UpdateLesson;

public sealed class UpdateLessonCommandHandler
    : IRequestHandler<UpdateLessonCommand, LessonDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateLessonCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LessonDto> Handle(
        UpdateLessonCommand request,
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

        var sectionExists = await _context.Sections
            .AnyAsync(
                section => section.Id == request.SectionId,
                cancellationToken);

        if (!sectionExists)
        {
            throw new KeyNotFoundException(
                $"Section with id '{request.SectionId}' was not found.");
        }

        lesson.SectionId = request.SectionId;
        lesson.Title = request.Title;
        lesson.Description = request.Description;
        lesson.Type = request.Type;
        lesson.VideoUrl = request.VideoUrl;
        lesson.Content = request.Content;
        lesson.DurationInSeconds = request.DurationInSeconds;
        lesson.DisplayOrder = request.DisplayOrder;
        lesson.IsFreePreview = request.IsFreePreview;
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