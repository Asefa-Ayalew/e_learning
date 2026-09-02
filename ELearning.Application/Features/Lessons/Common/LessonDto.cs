using ELearning.Domain.Enums;

namespace ELearning.Application.Features.Lessons.Common;

public sealed record LessonDto(
    Guid Id,
    Guid SectionId,
    string Title,
    string? Description,
    LessonType Type,
    string? VideoUrl,
    string? Content,
    int DurationInSeconds,
    int DisplayOrder,
    bool IsFreePreview,
    bool IsPublished,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);