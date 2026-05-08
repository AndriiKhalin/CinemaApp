using CinemaApi.Data;
using CinemaApi.DTOs.Seat;
using CinemaApi.DTOs.Session;
using CinemaApi.Interfaces;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessionsController(AppDbContext context, ISeatMapService seatMapService) : ControllerBase
{
    // GET /api/sessions
    [HttpGet]
    public async Task<ActionResult<List<SessionResponseDto>>> GetAll([FromQuery] int? movieId)
    {
        var query = context.Sessions
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .Where(s => s.StartTime > DateTime.UtcNow)
            .AsNoTracking();

        if (movieId.HasValue) query = query.Where(s => s.MovieId == movieId.Value);

        var sessions = await query.ToListAsync();

        var response = sessions.Select(s => new SessionResponseDto
        {
            Id = s.Id,
            StartTime = s.StartTime,
            TicketPrice = s.TicketPrice,
            MovieTitle = s.Movie?.Title ?? "Unknown Movie",
            HallName = s.Hall?.Name ?? "Unknown Hall"
        }).ToList();

        return Ok(response);
    }

    // GET /api/sessions/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SessionResponseDto>> GetById(int id)
    {
        var session = await context.Sessions
            .AsNoTracking()
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (session == null) return NotFound(new { message = $"Сеанс з ID {id} не знайдено." });

        return Ok(new SessionResponseDto
        {
            Id = session.Id,
            StartTime = session.StartTime,
            TicketPrice = session.TicketPrice,
            MovieTitle = session.Movie?.Title ?? "Unknown Movie",
            HallName = session.Hall?.Name ?? "Unknown Hall"
        });
    }

    // GET /api/sessions/{id}/seats
    [HttpGet("{id:int}/seats")]
    public async Task<ActionResult<SeatMapResponseDto>> GetSeats(int id)
    {
        var seatMap = await seatMapService.GetSeatMapAsync(id);

        if (seatMap == null)
            return NotFound(new { message = $"Сеанс з ID {id} не знайдено, неможливо отримати карту місць." });

        return Ok(seatMap);
    }

    // POST /api/sessions
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SessionRequestDto dto)
    {
        // Перевірка існування фільму та залу (дуже важливо для цілісності даних)
        var movieExists = await context.Movies.AnyAsync(m => m.Id == dto.MovieId);
        var hallExists = await context.Halls.AnyAsync(h => h.Id == dto.HallId);

        if (!movieExists) return BadRequest(new { message = "Вказаний фільм не знайдено." });
        if (!hallExists) return BadRequest(new { message = "Вказаний зал не знайдено." });

        var session = new Session
        {
            MovieId = dto.MovieId,
            HallId = dto.HallId,
            StartTime = dto.StartTime,
            TicketPrice = dto.TicketPrice
        };

        context.Sessions.Add(session);
        await context.SaveChangesAsync();

        // Повертаємо 201 Created та посилання на новий об'єкт
        return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
    }

    // DELETE /api/sessions/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var session = await context.Sessions.FindAsync(id);
        if (session == null) return NotFound();

        context.Sessions.Remove(session);
        await context.SaveChangesAsync();

        return NoContent();
    }
}