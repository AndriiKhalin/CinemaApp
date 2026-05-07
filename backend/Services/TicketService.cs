using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Services
{
    public class TicketService(AppDbContext context) : CinemaApi.Interfaces.ITicketService
    {
        public async Task<Ticket?> BookTicketAsync(Ticket ticket)
        {
 
            var isOccupied = await context.Tickets.AnyAsync(t =>
                t.SessionId == ticket.SessionId &&
                t.Row == ticket.Row &&
                t.SeatNumber == ticket.SeatNumber);

            if (isOccupied) return null;

            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();
            return ticket;
        }
    }
}