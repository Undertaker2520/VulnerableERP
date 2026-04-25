using Common.DTOs.Ticket;
using Common.Models;

namespace BusinessLayer.Interfaces;

public interface ITicketService
{
    Task<TicketResponse> CreateTicketAsync(CreateTicketRequest request, int userId);
    Task<List<TicketResponse>> GetAllTicketsAsync(int userId, bool isAdmin);
    Task<List<TicketResponse>> GetTicketAsync(string searchTerm, int userId, bool isAdmin);
}