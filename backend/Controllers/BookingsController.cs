using CinemaApi.Data;
using CinemaApi.DTOs.Booking;
using CinemaApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(AppDbContext context, ITicketService ticketService) : ControllerBase
    {
        // GET /api/bookings
        // Admin view — all bookings ordered newest first.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // REQUIREMENT: Return all bookings with Session → Movie and BookedSeats included.
            // Hint: .Include(b => b.Session).ThenInclude(s => s.Movie)
            //       .Include(b => b.BookedSeats)
            //       .OrderByDescending(b => b.CreatedAt)
            throw new NotImplementedException();
        }

        // GET /api/bookings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // REQUIREMENT: Return one booking with all related data included. 404 if missing.
            throw new NotImplementedException();
        }

        // POST /api/bookings
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            // REQUIREMENT: Prevent double-booking, save the booking, send a confirmation email.
            //
            // TODO: Validate request.Seats is not empty → BadRequest if empty.
            //
            // TODO: Check for seat conflicts — any seat in request.Seats already in BookedSeats
            //       for the same session → Conflict("One or more seats are already taken.")
            //       Hint: _context.BookedSeats.AnyAsync(bs =>
            //           bs.Booking.SessionId == request.SessionId &&
            //           request.Seats.Any(s => s.Row == bs.SeatRow && s.Number == bs.SeatNumber))
            //
            // TODO: Load the session to get TicketPrice for TotalPrice calculation.
            //
            // TODO: Build the Booking object with BookedSeats collection, add and SaveChangesAsync.
            //       TotalPrice = request.Seats.Count * session.TicketPrice
            //
            // TODO: Reload the booking with all related data so the email has Movie/Hall info.
            //       Hint: _context.Entry(booking).Reference(b => b.Session).LoadAsync()
            //
            // TODO: await _email.SendBookingConfirmationAsync(booking);
            //
            // TODO: Return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
            throw new NotImplementedException();
        }

        // DELETE /api/bookings/5
        // Cancel a booking.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            // REQUIREMENT: Delete a booking and all its BookedSeats (cascade delete).
            // EF Core handles cascade if the relationship is configured — just remove the Booking.
            // Return 404 if not found, 204 on success.
            throw new NotImplementedException();
        }
    }
}
