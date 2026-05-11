using CinemaApi.Data;
using CinemaApi.DTOs.Booking;
using CinemaApi.Interfaces;
using CinemaApi.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Services;

public class TicketService(AppDbContext context) : ITicketService
{
    public async Task<(bool Success, string? Error, IReadOnlyList<Ticket> Tickets)>
        BookTicketsAsync(CreateBookingRequest request, Session session, CancellationToken ct)
    {
        var seatKeys = request.Seats.Select(s => (s.Row, s.Number)).ToList();
        if (seatKeys.Count != seatKeys.Distinct().Count())
            return (false, "Duplicate seats in request", Array.Empty<Ticket>());

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
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException sqlite &&
                                           sqlite.SqliteErrorCode == 19)
        {
            return (false, "One or more selected seats are already booked", Array.Empty<Ticket>());
        }
    }
}