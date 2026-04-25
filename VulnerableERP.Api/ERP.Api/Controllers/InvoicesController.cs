using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService) => _invoiceService = invoiceService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

        if (invoice == null) return NotFound("Fattura non trovata.");

        return Ok(invoice);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceRequest request)
    {
        // Controllo Admin manuale (Vulnerabile a potenziali bypass logici)
        var isAdminClaim = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;
        if (isAdminClaim == null || !bool.Parse(isAdminClaim))
        {
            return StatusCode(403, "Solo gli amministratori possono generare fatture.");
        }

        var result = await _invoiceService.CreateInvoiceAsync(request);
        return Ok(result);
    }
}