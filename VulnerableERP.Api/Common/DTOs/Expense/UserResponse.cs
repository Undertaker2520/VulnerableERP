namespace Common.DTOs.Expense;
public class ExpenseResponse
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public int UserId { get; set; }
}