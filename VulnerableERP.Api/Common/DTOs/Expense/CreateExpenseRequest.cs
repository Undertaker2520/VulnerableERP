namespace Common.DTOs.Expense
{
    public class CreateExpenseRequest
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
