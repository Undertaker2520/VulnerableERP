using Common.DTOs.Ticket;
using Common.Models;

namespace BusinessLayer.Interfaces;

public interface ITicketService
{
    Task<TicketResponse> CreateTicketAsync(CreateTicketRequest request, int userId);
    Task<List<TicketResponse>> GetAllTicketsAsync();
    Task<List<TicketResponse>> GetTicketAsync(string searchTerm);
}