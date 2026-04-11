namespace Common.Models;

public class Invoice
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ConfidentialNotes { get; set; } = string.Empty; // Note che solo l'owner dovrebbe vedere

    // Riferimento all'utente proprietario
    public int UserId { get; set; }
    public User? User { get; set; }
}