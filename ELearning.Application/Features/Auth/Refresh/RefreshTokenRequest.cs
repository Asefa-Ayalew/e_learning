using System.ComponentModel.DataAnnotations;

namespace ELearning.Application.Features.Auth.Refresh;

public sealed class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}