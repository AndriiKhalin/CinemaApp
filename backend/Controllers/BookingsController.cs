using CinemaApi.Data;
using CinemaApi.DTOs.Booking;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(AppDbContext context) : ControllerBase
    {
        // GET: api/Bookings

        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> GetAll()
        {
            var tickets = await context.Tickets
                .AsNoTracking()
                .Include(t => t.Session)
                    .ThenInclude(s => s.Movie)
                .ToListAsync();

            return Ok(tickets);
        }

        // POST: api/Bookings

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            if (request.Seats == null || !request.Seats.Any())
                return BadRequest("Please select at least one seat.");

            var sessionExists = await context.Sessions.AnyAsync(s => s.Id == request.SessionId);
            if (!sessionExists) return NotFound("Session not found");

            foreach (var seat in request.Seats)
            {
                var ticket = new Ticket
                {
                    SessionId = request.SessionId,
                    Row = seat.Row,
                    SeatNumber = seat.Number,
                    CustomerEmail = request.Email,
                    CustomerName = "Guest", 
                    Session = null! 
                };
                context.Tickets.Add(ticket);
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("One or more selected seats are already booked");
            }

            return Ok(new { Message = "Booking completed successfully!" });
        }
    }
}