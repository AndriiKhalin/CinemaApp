namespace CinemaApi.Services;

public class SeatMapService
{
    // REQUIREMENT: Return enough data for JS to draw the seat map.
    // 1. Load the session with its Hall.
    // 2. Query all BookedSeats where Booking.SessionId == id.
    // 3. Return an anonymous object:
    //    { rows, seatsPerRow, takenSeats: [{seatRow, seatNumber}] }
    // Hint: _context.BookedSeats.Where(bs => bs.Booking.SessionId == id)
    //       .Select(bs => new { bs.SeatRow, bs.SeatNumber })
}