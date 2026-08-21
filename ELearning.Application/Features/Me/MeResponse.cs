namespace ELearning.Application.Features.Auth.Me;

public sealed record MeResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? ProfileImageUrl,
    bool IsActive,
    bool IsEmailVerified,
    DateTime? LastLoginAt,
    IReadOnlyCollection<string> Roles);