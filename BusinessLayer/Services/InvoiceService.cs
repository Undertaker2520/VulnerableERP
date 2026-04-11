using BusinessLayer.Interfaces;
using Common.DTOs;
using DataAccessLayer;

namespace BusinessLayer.Services;

public class InvoiceService : IInvoiceService
{
    private readonly AppDbContext _context;

    public InvoiceService(AppDbContext context)
    {
        _context = context;
    }

    public InvoiceResponse? GetInvoiceById(int id)
    {
        var invoice = _context.Invoices.Find(id);

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
}