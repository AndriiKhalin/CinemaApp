using CinemaApi.Data;
using CinemaApi.DTOs.Session;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessionsController(AppDbContext context) : ControllerBase
{
    // GET /api/sessions
    [HttpGet("/api/sessions")]
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
    [HttpGet("/api/sessions/{id:int}")]
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

    // POST /api/sessions
    [HttpPost("/api/admin/sessions")]
    public async Task<IActionResult> Create([FromBody] SessionRequestDto dto)
    {
        // Перевірка існування фільму та залу (дуже важливо для цілісності даних)
        var movieExists = await context.Movies.AnyAsync(m => m.Id == dto.MovieId);
        var hallExists = await context.Halls.AnyAsync(h => h.Id == dto.HallId);

        if (!movieExists) return BadRequest(new { message = "Вказаний фільм не знайдено." });
        if (!hallExists) return BadRequest(new { message = "Вказаний зал не знайдено." });

        var session = new Session
        {
            MovieId = dto.MovieId!.Value,
            HallId = dto.HallId!.Value,
            StartTime = dto.StartTime!.Value,
            TicketPrice = dto.TicketPrice
        };

        context.Sessions.Add(session);
        await context.SaveChangesAsync();

        var response = new SessionResponseDto
        {
            Id = session.Id,
            StartTime = session.StartTime,
            TicketPrice = session.TicketPrice,
            MovieTitle = await context.Movies.AsNoTracking().Where(m => m.Id == session.MovieId).Select(m => m.Title)
                .FirstAsync(),
            HallName = await context.Halls.AsNoTracking().Where(h => h.Id == session.HallId).Select(h => h.Name)
                .FirstAsync()
        };

        // Повертаємо 201 Created та посилання на новий об'єкт
        return CreatedAtAction(nameof(GetById), new { id = session.Id }, response);
    }

    [HttpPut("/api/admin/sessions/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SessionRequestDto dto)
    {
        var session = await context.Sessions.FindAsync(id);
        if (session == null) return NotFound(new { message = "Сеанс не знайдено." });

        var movieExists = await context.Movies.AnyAsync(m => m.Id == dto.MovieId);
        var hallExists = await context.Halls.AnyAsync(h => h.Id == dto.HallId);

        if (!movieExists) return BadRequest(new { message = "Вказаний фільм не знайдено." });
        if (!hallExists) return BadRequest(new { message = "Вказаний зал не знайдено." });

        session.MovieId = dto.MovieId!.Value;
        session.HallId = dto.HallId!.Value;
        session.StartTime = dto.StartTime!.Value;
        session.TicketPrice = dto.TicketPrice;

        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/admin/sessions/{id}
    [HttpDelete("/api/admin/sessions/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var session = await context.Sessions.FindAsync(id);
        if (session == null) return NotFound();

        context.Sessions.Remove(session);
        await context.SaveChangesAsync();

        return NoContent();
    }
}