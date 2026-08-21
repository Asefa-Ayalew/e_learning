using ELearning.Domain.Entities;

namespace ELearning.Application.Common.Authentication;

public interface IJwtTokenService
{
    AuthResponse GenerateTokens(User user);
}