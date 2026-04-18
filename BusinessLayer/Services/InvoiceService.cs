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

    public InvoiceResponse CreateInvoice(CreateInvoiceRequest request)
    {
        var invoice = new Invoice
        {
            Amount = request.Amount,
            Description = request.Description,
            ConfidentialNotes = request.ConfidentialNotes,
            UserId = request.UserId,
            Date = DateTime.UtcNow
        };

        _context.Invoices.Add(invoice);
        _context.SaveChanges();

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