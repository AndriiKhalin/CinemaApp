using CinemaApi.DTOs.Booking;
using CinemaApi.Models;

namespace CinemaApi.Interfaces;

public interface ITicketService
{
    Task<(bool Success, string? Error, IReadOnlyList<Ticket> Tickets)> BookTicketsAsync(CreateBookingRequest request,
        Session session, CancellationToken ct);
}