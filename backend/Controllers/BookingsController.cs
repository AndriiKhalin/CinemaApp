using CinemaApi.Data;
using CinemaApi.DTOs.Booking;
using CinemaApi.Interfaces;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(AppDbContext context, ITicketService ticketService, IEmailService emailService)
    : ControllerBase
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

            var booked = await ticketService.BookTicketAsync(ticket);
            if (booked == null)
                return BadRequest("One or more selected seats are already booked");

            await emailService.SendBookingConfirmationAsync(booked);
        }

        return Ok(new { Message = "Booking completed successfully!" });
    }
}