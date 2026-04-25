using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Common.DTOs.Ticket;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService) => _ticketService = ticketService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized("Invalid token or not recognizable user.");

        var result = await _ticketService.CreateTicketAsync(request, userId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        var tickets = await _ticketService.GetAllTicketsAsync(userId, roleClaim == "Admin");
        return Ok(tickets);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromQuery] string q)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        var tickets = await _ticketService.GetTicketAsync(q, userId, roleClaim == "Admin");
        return Ok(tickets);
    }
}