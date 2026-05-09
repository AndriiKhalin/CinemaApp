using CinemaApi.Data;
using CinemaApi.DTOs.Booking;
using CinemaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(AppDbContext context, ITicketService ticketService, IEmailService emailService)
    : ControllerBase
{
    // GET: api/Bookings
    [HttpGet("/api/admin/bookings")]
    public async Task<ActionResult<List<BookingResponseDto>>> GetAll()
    {
        var tickets = await context.Tickets
            .AsNoTracking()
            .Include(t => t.Session)
            .ThenInclude(s => s.Movie)
            .Select(t => new BookingResponseDto
            {
                TicketId = t.Id,
                SessionId = t.SessionId,
                Row = t.Row,
                SeatNumber = t.SeatNumber,
                PurchasedAt = t.CreateDateTime
            })
            .ToListAsync();

        return Ok(tickets);
    }

    // POST: api/Bookings

    [HttpPost("/api/bookings")]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (request.Seats == null || !request.Seats.Any())
            return BadRequest("Please select at least one seat.");

        var session = await context.Sessions.FirstOrDefaultAsync(s => s.Id == request.SessionId, ct);
        if (session == null) return NotFound("Session not found");

        var result = await ticketService.BookTicketsAsync(request, session, ct);
        if (!result.Success) return BadRequest(result.Error);

        foreach (var ticket in result.Tickets)
            await emailService.SendBookingConfirmationAsync(ticket);

        return Ok(new { Message = "Booking completed successfully!" });
    }
}