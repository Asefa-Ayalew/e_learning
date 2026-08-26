using ELearning.Application.Common.Authentication;
using ELearning.Application.Common.Authorization;
using ELearning.Application.Features.Auth;
using ELearning.Application.Features.Auth.Login;
using ELearning.Application.Features.Auth.Me;
using ELearning.Application.Features.Auth.Refresh;
using ELearning.Application.Features.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    // For test
    // [HttpGet("Admin")]
    // [Authorize(Policy = AuthorizationPolicies.RequireAdmin)]
    // public IActionResult Admin()
    // {
    //     return Ok("You have Admin Access.");
    // }
    // [HttpGet("Student")]
    // [Authorize(Policy = AuthorizationPolicies.RequireStudent)]
    // public IActionResult Student()
    // {
    //     return Ok("You have Student Access.");
    // }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> Refresh(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshAsync(
            request.RefreshToken,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        await _authService.RevokeAsync(
            request.RefreshToken,
            cancellationToken);

        return NoContent();
    }
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<MeResponse>> Me(
    CancellationToken cancellationToken)
    {
        var userIdValue = User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var user = await _authService.GetCurrentUserAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}