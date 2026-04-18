using Common.DTOs;
using Common.DTOs.Invoice;
namespace BusinessLayer.Interfaces;

public interface IInvoiceService
{
    InvoiceResponse? GetInvoiceById(int id);
    InvoiceResponse CreateInvoice(CreateInvoiceRequest request);
}