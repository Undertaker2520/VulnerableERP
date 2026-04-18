using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.Invoice;
using Common.Models;
using DataAccessLayer;

namespace BusinessLayer.Services;

public class InvoiceService : IInvoiceService
{
    private readonly AppDbContext _context;

    public InvoiceService(AppDbContext context) => _context = context;

    public async Task<InvoiceResponse?> GetInvoiceByIdAsync(int id)
    {
        // VULNERABILITÀ IDOR: Nessun controllo di proprietà sull'utente.
        // FindAsync è perfetto per recuperare l'entità in modo non bloccante.
        var invoice = await _context.Invoices.FindAsync(id);

        if (invoice == null) return null;

        return new InvoiceResponse
        {
            Id = invoice.Id,
            Amount = invoice.Amount,
            Date = invoice.Date,
            Description = invoice.Description,
            ConfidentialNotes = invoice.ConfidentialNotes
        };
    }

    public async Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        var invoice = new Invoice
        {
            Amount = request.Amount,
            Description = request.Description,
            ConfidentialNotes = request.ConfidentialNotes,
            UserId = request.UserId,
            Date = DateTime.UtcNow
        };

        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();

        return new InvoiceResponse
        {
            Id = invoice.Id,
            Amount = invoice.Amount,
            Description = invoice.Description,
            ConfidentialNotes = invoice.ConfidentialNotes,
            Date = invoice.Date
        };
    }
}