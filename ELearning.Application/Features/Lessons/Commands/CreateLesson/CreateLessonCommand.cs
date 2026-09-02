using ELearning.Domain.Enums;
using MediatR;

namespace ELearning.Application.Features.Lessons.Commands.CreateLesson;

public sealed record CreateLessonCommand(
    Guid SectionId,
    string Title,
    string? Description,
    LessonType Type,
    string? VideoUrl,
    string? Content,
    int DurationInSeconds,
    int DisplayOrder,
    bool IsFreePreview
    ) : IRequest<Guid>;