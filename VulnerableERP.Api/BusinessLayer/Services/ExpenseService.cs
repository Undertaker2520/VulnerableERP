using BusinessLayer.Interfaces;
using Common.DTOs.Expense;
using Common.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
namespace BusinessLayer.Services;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;

    public ExpenseService(AppDbContext context) => _context = context;

    public async Task<ExpenseResponse> CreateExpenseAsync(CreateExpenseRequest request, int userId)
    {
        var expense = new Expense
        {
            Description = request.Description,
            Amount = request.Amount,
            UserId = userId,
            Status = "Pending"
        };

        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();

        return new ExpenseResponse
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            Status = expense.Status
        };
    }

    public async Task<IEnumerable<ExpenseResponse>> GetUserExpensesAsync(int userId)
    {
        return await _context.Expenses
            .Where(e => e.UserId == userId)
            .Select(e => new ExpenseResponse
            {
                Id = e.Id,
                Description = e.Description,
                Amount = e.Amount,
                Status = e.Status,
                Date = e.CreatedAt.ToString("yyyy-MM-dd"), // Aggiungiamo la data per coerenza con l'UI
                UserId = e.UserId
            }).ToListAsync();
    }

    public async Task<IEnumerable<ExpenseResponse>> GetAllExpensesAsync()
    {
        return await _context.Expenses
            .Select(e => new ExpenseResponse
            {
                Id = e.Id,
                Description = e.Description,
                Amount = e.Amount,
                Status = e.Status,
                Date = e.CreatedAt.ToString("yyyy-MM-dd"),
                UserId = e.UserId
            }).ToListAsync();
    }

    public async Task<string> UpdateStatusAsync(UpdateExpenseStatusRequest request)
    {
        var expense = await _context.Expenses.FindAsync(request.ExpenseId);
        if (expense == null)
            return "Nota spese non trovata";

        //VULNERABILITA': LOGICA DI BUSINESS DIFETTOSA
        //Il codice cambia lo stato basandosi puramente sulla richiesta del client.
        //Non controlla se l'utente attuale è admin o il proprietario della richiesta.
        expense.Status = request.NewStatus;

        await _context.SaveChangesAsync();
        return $"Stato aggiornato a {request.NewStatus}";
    }
}