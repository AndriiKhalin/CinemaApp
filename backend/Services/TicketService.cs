using CinemaApi.Data;
using CinemaApi.DTOs.Booking;
using CinemaApi.Interfaces;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Services;

public class TicketService(AppDbContext context) : ITicketService
{
    public async Task<(bool Success, string? Error, IReadOnlyList<Ticket> Tickets)>
        BookTicketsAsync(CreateBookingRequest request, Session session, CancellationToken ct)
    {
        var tickets = request.Seats.Select(seat => new Ticket
        {
            SessionId = session.Id,
            Row = seat.Row,
            SeatNumber = seat.Number,
            CustomerEmail = request.Email,
            CustomerName = request.CustomerName,
            Session = session,
            CreateDateTime = DateTime.UtcNow
        }).ToList();

        context.Tickets.AddRange(tickets);

        try
        {
            await context.SaveChangesAsync(ct);
            return (true, null, tickets);
        }
        catch (DbUpdateException)
        {
            return (false, "One or more selected seats are already booked", Array.Empty<Ticket>());
        }
    }
}