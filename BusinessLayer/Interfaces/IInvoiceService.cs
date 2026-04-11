using Common.DTOs;
namespace BusinessLayer.Interfaces;

public interface IInvoiceService
{
    InvoiceResponse? GetInvoiceById(int id);
}