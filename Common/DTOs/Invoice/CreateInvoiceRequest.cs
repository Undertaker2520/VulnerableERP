namespace Common.DTOs;

public class CreateInvoiceRequest
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ConfidentialNotes { get; set; } = string.Empty;
    public int UserId { get; set; } // ID dell'utente a cui assegnare la fattura
}