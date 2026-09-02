using ELearning.Application.Features.Lessons.Common;
using ELearning.Domain.Enums;
using MediatR;

namespace ELearning.Application.Features.Lessons.Commands.UpdateLesson;

public sealed record UpdateLessonCommand(
    Guid Id,
    Guid SectionId,
    string Title,
    string? Description,
    LessonType Type,
    string? VideoUrl,
    string? Content,
    int DurationInSeconds,
    int DisplayOrder,
    bool IsFreePreview
) : IRequest<LessonDto>;