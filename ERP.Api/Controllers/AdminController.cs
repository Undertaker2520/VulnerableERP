using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] //vulnerabilitá logica: richiede solo di essere loggati ma la verifica é dentro
public class AdminController : ControllerBase
{
    [HttpGet("dashboard")]
    public IActionResult GetDashBoardData()
    {
        var isAdminClaim = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;

        if (isAdminClaim != null && bool.TryParse(isAdminClaim, out bool isAdmin) && isAdmin)
        {
            // Return some dummy admin data
            return Ok(new
            {
                Message = "Benvenuto nell'area protetta Amministratore",
                ConfidentialData = "I profitti aziendali di quest'anno sono di 5 milioni di euro. Coordinate bancarie: IT99C..."
            });
        }

        //se non é admin, anche se é loggato, non ha accesso
        return StatusCode(403, new { Message = "Accesso negato. Solo gli amministratori possono accedere a questa risorsa." });
    }
} 