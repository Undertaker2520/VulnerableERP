using BusinessLayer.Interfaces;
using Common.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // Attendiamo il risultato del servizio
        var result = await _authService.RegisterUserAsync(request);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(new
        {
            Message = result.Message,
            UserId = result.User?.id,
            IsAdmin = result.User?.isAdmin
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        if (!result.Success)
        {
            return Unauthorized(new { Message = result.Message });
        }

        return Ok(new { Token = result.Token });
    }
}