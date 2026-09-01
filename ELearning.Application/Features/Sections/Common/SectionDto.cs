namespace ELearning.Application.Features.Sections.Common;

public sealed record SectionDto(
    Guid Id,
    Guid CourseId,
    string Title,
    string? Description,
    int DisplayOrder,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);