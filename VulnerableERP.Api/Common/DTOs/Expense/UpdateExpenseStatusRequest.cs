namespace Common.DTOs.Expense
{
    public class UpdateExpenseStatusRequest
    {
        public int ExpenseId { get; set; }
        public string NewStatus { get; set; } = string.Empty;
    }
}
