using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Common.DTOs.Ticket;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] //assicura che l'utente sia loggato
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    
    public TicketsController(ITicketService ticketService) => _ticketService = ticketService;

    [HttpPost]
    public IActionResult Create([FromBody] CreateTicketRequest request)
    {
       //Estraiamo l'ID dell'utente loggato dal token JWT
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if(!int.TryParse(userIdClaim, out int userId))
            return Unauthorized("Invalid token or not recognizable user.");

        var result = _ticketService.CreateTicket(request, userId);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var tickets = _ticketService.GetAllTickets();
        return Ok(tickets);
    }

    [HttpGet("search")]
    public IActionResult Get([FromQuery] string q)
    {
        var tickets = _ticketService.GetTicket(q);
        return Ok(tickets);
    }
} 