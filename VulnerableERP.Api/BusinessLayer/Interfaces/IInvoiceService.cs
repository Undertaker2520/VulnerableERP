using Common.DTOs;
using Common.DTOs.Invoice;

namespace BusinessLayer.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceResponse?> GetInvoiceByIdAsync(int id);
    Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request);
}