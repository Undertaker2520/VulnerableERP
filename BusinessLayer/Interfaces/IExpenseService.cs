using Common.DTOs.Expense;
namespace BusinessLayer.Interfaces;

public interface IExpenseService
{
    Task<string> UpdateStatusAsync(UpdateExpenseStatusRequest request);
    Task<IEnumerable<ExpenseResponse>> GetUserExpensesAsync(int userId);
    Task<ExpenseResponse> CreateExpenseAsync(CreateExpenseRequest request, int userId);
}