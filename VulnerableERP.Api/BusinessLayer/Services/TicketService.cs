using BusinessLayer.Interfaces;
using Common.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Common.DTOs.Ticket;

namespace BusinessLayer.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context) => _context = context;

    public async Task<TicketResponse> CreateTicketAsync(CreateTicketRequest request, int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        var ticket = new Ticket()
        {
            Title = request.Title,
            // VULNERABILITÀ 1: STORED XSS (Nessuna sanitizzazione)
            Description = request.Description,
            UserId = userId,
        };

        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();

        return new TicketResponse
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Username = user.Username,
            CreatedAt = ticket.CreatedAt
        };
    }

    public async Task<List<TicketResponse>> GetAllTicketsAsync(int userId, bool isAdmin)
    {
        var query = _context.Tickets.Include(t => t.User).AsQueryable();

        if (!isAdmin)
        {
            query = query.Where(t => t.UserId == userId);
        }

        return await query
            .Select(t => new TicketResponse
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Username = t.User != null ? t.User.Username : "Unknown",
                CreatedAt = t.CreatedAt
            }).ToListAsync();
    }

    public async Task<List<TicketResponse>> GetTicketAsync(string searchTerm, int userId, bool isAdmin)
    {
        // VULNERABILITÀ 2: SQL INJECTION (Time-Based)
        // Se non siamo admin, proviamo ad aggiungere un filtro manuale vulnerabile
        string rawSql;
        if (isAdmin)
        {
            rawSql = "SELECT * FROM Tickets WHERE Title LIKE '%" + searchTerm + "%'";
        }
        else
        {
            // Nota: Anche qui la concatenazione è vulnerabile, permettendo di bypassare il filtro UserId
            rawSql = "SELECT * FROM Tickets WHERE UserId = " + userId + " AND Title LIKE '%" + searchTerm + "%'";
        }

        var tickets = await _context.Tickets.FromSqlRaw(rawSql).ToListAsync();

        return tickets.Select(t => new TicketResponse
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Username = t.UserId.ToString(),
            CreatedAt = t.CreatedAt
        }).ToList();
    }
}