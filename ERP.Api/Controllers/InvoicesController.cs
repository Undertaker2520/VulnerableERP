using BusinessLayer.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] // L'utente deve essere loggato, ma l'IDOR permette di vedere fatture altrui
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService) => _invoiceService = invoiceService;

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var invoice = _invoiceService.GetInvoiceById(id);

        if (invoice == null) return NotFound("Fattura non trovata.");

        return Ok(invoice);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateInvoiceRequest request)
    {
        // Controllo Admin
        var isAdmin = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;
        if (isAdmin == null || !bool.Parse(isAdmin))
        {
            return StatusCode(403, "Solo gli amministratori possono generare fatture.");
        }

        var result = _invoiceService.CreateInvoice(request);
        return Ok(result);
    }
}