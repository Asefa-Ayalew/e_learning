namespace ELearning.Application.Features.Courses.Common;

public sealed record CourseResponseDto(
    Guid Id,
    string Title,
    string Slug,
    string? ShortDescription,
    string? Description,
    string? ThumbnailUrl,
    decimal Price,
    bool IsFree,
    bool IsPublished,
    DateTime? PublishedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    Guid InstructorId,
    string InstructorName,
    Guid CategoryId,
    string CategoryName);