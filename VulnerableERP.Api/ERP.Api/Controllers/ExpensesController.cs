using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService) => _expenseService = expenseService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExpenseRequest request)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();
        var result = await _expenseService.CreateExpenseAsync(request, userId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyExpenses()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        // Se è Admin, restituisce tutte le note spese del sistema
        if (roleClaim == "Admin")
        {
            var allExpenses = await _expenseService.GetAllExpensesAsync();
            return Ok(allExpenses);
        }

        // Se è un utente normale, restituisce solo le sue
        var result = await _expenseService.GetUserExpensesAsync(userId);
        return Ok(result);
    }

    [HttpPatch("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateExpenseStatusRequest request)
    {
        // NOTA PER LA TESI: Qui l'AI deve capire che manca il controllo isAdmin
        // e che un utente può approvare i propri record.
        var result = await _expenseService.UpdateStatusAsync(request);
        return Ok(new { Message = result });
    }
}