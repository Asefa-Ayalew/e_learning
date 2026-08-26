namespace ELearning.Application.Features.Users.GetUser;

public sealed record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? ProfileImageUrl,
    bool IsActive,
    bool IsEmailVerified,
    DateTime? LastLoginAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyCollection<string> Roles);