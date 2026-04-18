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

    // Iniettiamo l'interfaccia del servizio, non più il DbContext!
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        var result = _authService.RegisterUser(request);

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
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var result = _authService.Login(request);

        if (!result.Success)
        {
            return Unauthorized(new { Message = result.Message });
        }

        return Ok(new { Token = result.Token });
    }
}