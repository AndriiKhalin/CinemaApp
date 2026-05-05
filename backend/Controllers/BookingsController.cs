using CinemaApi.Data;
using CinemaApi.DTOs.Booking;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            if (request.Seats == null || !request.Seats.Any())
                return BadRequest("You need to select at least one seat.");

            var session = await _context.Sessions
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == request.SessionId);

            if (session == null) return NotFound("Session not found.");

            var booking = new Booking
            {
                SessionId = request.SessionId,
                CustomerEmail = request.Email,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = request.Seats.Count * session.TicketPrice,
                BookedSeats = request.Seats.Select(s => new BookedSeat
                {
                    SeatRow = s.Row,
                    SeatNumber = s.Number
                }).ToList()
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Booking created.", BookingId = booking.Id });
        }
    }
}