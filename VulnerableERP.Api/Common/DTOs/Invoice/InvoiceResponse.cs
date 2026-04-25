namespace Common.DTOs.Invoice;

public class InvoiceResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ConfidentialNotes { get; set; } = string.Empty;
}

