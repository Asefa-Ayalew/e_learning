using ELearning.Application.Features.Users.GetUser;
using ELearning.Application.Features.Users.UpdateUser;

namespace ELearning.Application.Features.Users;

public interface IUserService
{
    Task<UserResponse?> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<UserResponse?> UpdateAsync(
        Guid userId,
        UpdateUserRequest request,
        CancellationToken cancellationToken = default);
}