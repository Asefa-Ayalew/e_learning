using ELearning.Application.Interfaces;
using ELearning.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Lessons.Commands.CreateLesson;

public sealed class CreateLessonCommandHandler
: IRequestHandler<CreateLessonCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateLessonCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(
        CreateLessonCommand request,
        CancellationToken cancellationToken
    )
    {
        var sectionExists = await _context.Sections
        .AnyAsync(section => section.Id == request.SectionId, cancellationToken);
        if (!sectionExists)
        {
            throw new ArgumentException("Section not exist.");
        }
        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            SectionId = request.SectionId,
            Title = request.Title,
            Description = request.Description,
            Type = request.Type,
            VideoUrl = request.VideoUrl,
            Content = request.Content,
            DurationInSeconds = request.DurationInSeconds,
            DisplayOrder = request.DisplayOrder,
            IsFreePreview = request.IsFreePreview,
            IsPublished = false,
            CreatedAt = DateTime.UtcNow,
        };
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        return lesson.Id;
    }
}