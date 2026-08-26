using ELearning.Application.Features.Users.GetUser;
using ELearning.Application.Features.Users.UpdateUser;
using ELearning.Application.Interfaces;
using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Application.Features.Users;

public sealed class UserService : IUserService
{
    private readonly IApplicationDbContext _dbContext;

    public UserService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserResponse?> GetByIdAsync(
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

        return MapToResponse(user);
    }

    public async Task<UserResponse?> UpdateAsync(
        Guid userId,
        UpdateUserRequest request,
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

        user.FirstName = request.FirstName.Trim();

        user.LastName = request.LastName.Trim();

        user.PhoneNumber = request.PhoneNumber?.Trim();

        user.ProfileImageUrl =
            request.ProfileImageUrl?.Trim();

        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return MapToResponse(user);
    }

    private static UserResponse MapToResponse(
        User user)
    {
        var roles = user.UserRoles
            .Where(userRole =>
                userRole.Role is not null &&
                userRole.Role.IsActive)
            .Select(userRole => userRole.Role!.Name)
            .ToList();

        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.PhoneNumber,
            user.ProfileImageUrl,
            user.IsActive,
            user.IsEmailVerified,
            user.LastLoginAt,
            user.CreatedAt,
            user.UpdatedAt,
            roles);
    }
}