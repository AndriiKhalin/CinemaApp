using CinemaApi.Data;
using CinemaApi.DTOs.Seat;
using CinemaApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Services;

public class SeatMapService(AppDbContext context) : ISeatMapService
{
    public async Task<SeatMapResponseDto?> GetSeatMapAsync(int sessionId)
    {
        var availableSeats = new List<SeatSelectionDto>();

        var session = await context.Sessions
            .Include(s => s.Hall)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null || session.Hall == null) return null;

        var bookedSeats = await context.Tickets
            .Where(t => t.SessionId == sessionId)
            .Select(t => new SeatSelectionDto
            {
                Row = t.Row,
                Number = t.SeatNumber,
                IsAvailable = false
            })
            .ToListAsync();

        var bookedSet = bookedSeats.Select(b => (b.Row, b.Number)).ToHashSet();

        for (var r = 1; r <= session.Hall.TotalRows; r++)
        for (var s = 1; s <= session.Hall.SeatsPerRow; s++)
            if (!bookedSet.Contains((r, s)))
                availableSeats.Add(new SeatSelectionDto { Row = r, Number = s, IsAvailable = true });

        return new SeatMapResponseDto
        {
            TotalRows = session.Hall.TotalRows,
            SeatsPerRow = session.Hall.SeatsPerRow,
            BookedSeats = bookedSeats,
            AvailableSeats = availableSeats
        };
    }
}