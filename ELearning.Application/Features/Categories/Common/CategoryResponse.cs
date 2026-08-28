namespace ELearning.Application.Features.Categories.Common;

public sealed record CategoryResponse(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);