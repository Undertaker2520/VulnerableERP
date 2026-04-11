using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] // L'utente deve essere loggato, ma l'IDOR permette di vedere fatture altrui
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var invoice = _invoiceService.GetInvoiceById(id);

        if (invoice == null) return NotFound("Fattura non trovata.");

        return Ok(invoice);
    }
}