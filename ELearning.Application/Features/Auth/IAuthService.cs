using ELearning.Application.Common.Authentication;
using ELearning.Application.Features.Auth.Login;
using ELearning.Application.Features.Auth.Register;

namespace ELearning.Application.Features.Auth;

public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default);

    Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);

    Task<AuthResponse> RefreshAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}