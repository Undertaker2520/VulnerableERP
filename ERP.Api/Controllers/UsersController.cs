using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var isAdminClaim = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;
        if (isAdminClaim != null && bool.TryParse(isAdminClaim, out bool isAdmin) && isAdmin)
        {
            return Ok(_userService.GetAllUsers());
        }

        return StatusCode(403, "Accesso negato: richiesti privilegi di amministratore.");

    }
}