using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ELearning.Application.Common.Authentication;
using ELearning.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ELearning.Infrastructure.Authentication;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public AuthResponse GenerateTokens(User user)
    {
        // ============================================================
        // ACCESS TOKEN
        // ============================================================

        var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
            _jwtSettings.AccessTokenExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),

            new("firstName", user.FirstName),
            new("lastName", user.LastName)
        };

        // Add the user's roles to the JWT.
        foreach (var userRole in user.UserRoles)
        {
            if (userRole.Role is not null)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        userRole.Role.Name));
            }
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: accessTokenExpiresAt,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(jwt);

        // ============================================================
        // REFRESH TOKEN
        // ============================================================

        var refreshToken = GenerateRefreshToken();

        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(
            _jwtSettings.RefreshTokenExpirationDays);

        return new AuthResponse(
            AccessToken: accessToken,
            AccessTokenExpiresAt: accessTokenExpiresAt,
            RefreshToken: refreshToken,
            RefreshTokenExpiresAt: refreshTokenExpiresAt);
    }

    private static string GenerateRefreshToken()
    {
        // Cryptographically secure random bytes.
        var bytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(bytes);
    }
}