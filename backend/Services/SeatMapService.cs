using CinemaApi.Data;
using CinemaApi.DTOs.Seat;
using CinemaApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Services;

public class SeatMapService(AppDbContext context) : ISeatMapService
{
    public async Task<SeatMapResponseDto?> GetSeatMapAsync(int sessionId)
    {
        var session = await context.Sessions
            .Include(s => s.Hall)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null || session.Hall == null) return null;

        var takenSeats = await context.Tickets
            .Where(t => t.SessionId == sessionId)
            .Select(t => new SeatSelectionDto
            {
                Row = t.Row,
                Number = t.SeatNumber
            })
            .ToListAsync();


        return new SeatMapResponseDto
        {
            TotalRows = session.Hall.TotalRows,
            SeatsPerRow = session.Hall.SeatsPerRow,
            BookedSeats = takenSeats
        };
    }
}