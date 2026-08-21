using ELearning.Application.Common.Authentication;
using ELearning.Application.Features.Auth.Login;
using ELearning.Application.Features.Auth.Register;
using ELearning.Application.Interfaces;
using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ELearning.Application.Features.Auth.Me;

namespace ELearning.Application.Features.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IApplicationDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<RegisterResponse> RegisterAsync(
     RegisterRequest request,
     CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(
                user => user.Email == email,
                cancellationToken);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var studentRole = await _dbContext.Roles
            .FirstOrDefaultAsync(
                role =>
                    role.Name == "Student" &&
                    role.IsActive,
                cancellationToken);

        if (studentRole is null)
        {
            throw new InvalidOperationException(
                "The Student role is not configured.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),

            FirstName = request.FirstName.Trim(),

            LastName = request.LastName.Trim(),

            Email = email,

            Password = _passwordHasher.Hash(request.Password),

            PhoneNumber = request.PhoneNumber?.Trim(),

            IsActive = true,

            IsEmailVerified = false,

            CreatedAt = DateTime.UtcNow
        };

        var userRole = new UserRole
        {
            UserId = user.Id,

            RoleId = studentRole.Id,

            User = user,

            Role = studentRole,

            AssignedAt = DateTime.UtcNow
        };

        user.UserRoles.Add(userRole);

        _dbContext.Users.Add(user);
        _dbContext.UserRoles.Add(userRole);

        // Generate authentication tokens after assigning the role.
        // JwtTokenService reads user.UserRoles and adds role claims.
        var authResult = _jwtTokenService.GenerateTokens(user);

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),

            UserId = user.Id,

            Token = authResult.RefreshToken,

            ExpiresAt = authResult.RefreshTokenExpiresAt,

            CreatedAt = DateTime.UtcNow
        };

        _dbContext.RefreshTokens.Add(refreshToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterResponse(
            user.Id,
            new AuthResponse(
                authResult.AccessToken,
                authResult.AccessTokenExpiresAt,
                authResult.RefreshToken,
                authResult.RefreshTokenExpiresAt));
    }
    public async Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _dbContext.Users
            .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
            .FirstOrDefaultAsync(
                user => user.Email == email,
                cancellationToken);

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "This account is inactive.");
        }

        var passwordValid = _passwordHasher.Verify(
            request.Password,
            user.Password);

        if (!passwordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var authResult = _jwtTokenService.GenerateTokens(user);

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),

            UserId = user.Id,

            Token = authResult.RefreshToken,

            ExpiresAt = authResult.RefreshTokenExpiresAt,

            CreatedAt = DateTime.UtcNow
        };

        _dbContext.RefreshTokens.Add(refreshToken);

        user.LastLoginAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AuthResponse(
            authResult.AccessToken,
            authResult.AccessTokenExpiresAt,
            authResult.RefreshToken,
            authResult.RefreshTokenExpiresAt);
    }

    public async Task<AuthResponse> RefreshAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var storedToken = await _dbContext.RefreshTokens
            .Include(token => token.User)
                .ThenInclude(user => user.UserRoles)
                    .ThenInclude(userRole => userRole.Role)
            .FirstOrDefaultAsync(
                token => token.Token == refreshToken,
                cancellationToken);

        if (storedToken is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid refresh token.");
        }

        if (storedToken.IsRevoked)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has been revoked.");
        }

        if (storedToken.IsExpired)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has expired.");
        }

        if (!storedToken.User.IsActive)
        {
            throw new UnauthorizedAccessException(
                "This account is inactive.");
        }

        // Generate a completely new access + refresh token pair.
        var authResult = _jwtTokenService.GenerateTokens(
            storedToken.User);

        // Revoke the old refresh token.
        storedToken.RevokedAt = DateTime.UtcNow;

        storedToken.ReplacedByToken = authResult.RefreshToken;

        // Store the new refresh token.
        var replacementToken = new RefreshToken
        {
            Id = Guid.NewGuid(),

            UserId = storedToken.UserId,

            Token = authResult.RefreshToken,

            ExpiresAt = authResult.RefreshTokenExpiresAt,

            CreatedAt = DateTime.UtcNow
        };

        _dbContext.RefreshTokens.Add(replacementToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AuthResponse(
            authResult.AccessToken,
            authResult.AccessTokenExpiresAt,
            authResult.RefreshToken,
            authResult.RefreshTokenExpiresAt);
    }

    public async Task RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var storedToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(
                token => token.Token == refreshToken,
                cancellationToken);

        if (storedToken is null)
        {
            return;
        }

        if (!storedToken.IsRevoked)
        {
            storedToken.RevokedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    // ================================================================
    // CURRENT USER
    // ================================================================
    //
    // Gets the currently authenticated user.
    //
    // The API extracts the user's ID from the JWT:
    //
    //     JWT
    //       ↓
    // HttpContext.User
    //       ↓
    // user ID
    //       ↓
    // Database
    //       ↓
    // MeResponse
    //
    // ================================================================

    public async Task<MeResponse?> GetCurrentUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
            .FirstOrDefaultAsync(
                user => user.Id == userId,
                cancellationToken);

        if (user is null)
        {
            return null;
        }

        var roles = user.UserRoles
            .Where(userRole =>
                userRole.Role is not null &&
                userRole.Role.IsActive)
            .Select(userRole => userRole.Role!.Name)
            .ToList();

        return new MeResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.ProfileImageUrl,
            user.IsActive,
            user.IsEmailVerified,
            user.LastLoginAt,
            roles);
    }
}