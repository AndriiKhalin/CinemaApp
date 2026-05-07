using CinemaApi.Models;

namespace CinemaApi.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket?> BookTicketAsync(Ticket ticket);
    }
}