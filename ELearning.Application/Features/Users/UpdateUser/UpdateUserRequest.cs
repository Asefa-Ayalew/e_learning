using System.ComponentModel.DataAnnotations;

namespace ELearning.Application.Features.Users.UpdateUser;

public sealed record UpdateUserRequest(
    [property: Required]
    [property: MaxLength(100)]
    string FirstName,

    [property: Required]
    [property: MaxLength(100)]
    string LastName,

    [property: MaxLength(30)]
    string? PhoneNumber,
    [property: MaxLength(500)]
    string? ProfileImageUrl);