using ELearning.Application.Interfaces;
using ELearning.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Sections.Commands.CreateSection;

public sealed class CreateSectionCommandHandler
    : IRequestHandler<CreateSectionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSectionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateSectionCommand request,
        CancellationToken cancellationToken)
    {

        var courseExists = await _context.Courses
            .AnyAsync(
                course => course.Id == request.CourseId,
                cancellationToken);

        if (!courseExists)
        {
            throw new KeyNotFoundException(
                $"Course with ID '{request.CourseId}' was not found.");
        }


        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException(
                "Section title is required.");
        }

        if (request.DisplayOrder < 1)
        {
            throw new ArgumentException(
                "Display order must be greater than or equal to 1.");
        }

        var displayOrderExists = await _context.Sections
            .AnyAsync(
                section =>
                    section.CourseId == request.CourseId &&
                    section.DisplayOrder == request.DisplayOrder,
                cancellationToken);

        if (displayOrderExists)
        {
            throw new InvalidOperationException(
                $"A section with display order '{request.DisplayOrder}' " +
                "already exists for this course.");
        }

        var section = new Section
        {
            Id = Guid.NewGuid(),
            CourseId = request.CourseId,
            Title = request.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim(),
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow
        };

        _context.Sections.Add(section);

        await _context.SaveChangesAsync(cancellationToken);

        return section.Id;
    }
}
