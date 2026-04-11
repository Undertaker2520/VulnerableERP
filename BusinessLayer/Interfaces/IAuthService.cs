using Common.Models;

namespace BusinessLayer.Interfaces;

public interface ITicketService
{
    TicketResponse CreateTicket(CreateTicketRequest request, int userId);
    List<TicketResponse> GetAllTickets();
    List<TicketResponse> GetTicket(string searchTerm);
}