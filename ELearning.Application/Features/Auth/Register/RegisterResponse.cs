
using ELearning.Application.Common.Authentication;

namespace ELearning.Application.Features.Auth.Register;

public sealed record RegisterResponse(
    Guid UserId,
    AuthResponse Authentication);