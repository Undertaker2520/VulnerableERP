using BusinessLayer.Interfaces;
using Common.Models;
using DataAccessLayer;
using Authentication;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public TicketResponse CreateTicket(CreateTicketRequest request, int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) 
                throw new Exception("User not found");

            var ticket = new Ticket()
            {
                Title = request.Title,
                Description = request.Description,
                UserId = userId,
            }; 

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return new TicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Username = user.Username,
                CreatedAt = ticket.CreatedAt
            };
        }

        public List<TicketResponse> GetAllTickets()
        {
            return _context.Tickets
                .Include(t => t.User)
                .Select(t => new TicketResponse
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description, // return payload to client 
                Username = t.User != null ? t.User.Username : "Unknown",
                    CreatedAt = t.CreatedAt
            }).ToList();
        }

        public List<TicketResponse> GetTicket(string searchTerm)
        {
            string rawSql = "SELECT * FROM Tickets WHERE Title LIKE '%" + searchTerm + "%'";

            var tickets = _context.Tickets.FromSqlRaw(rawSql).ToList();

            return tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description, 
                Username = t.UserId.ToString(), // semplified for raw query
                CreatedAt = t.CreatedAt
            }).ToList();
        }
    }
} 
