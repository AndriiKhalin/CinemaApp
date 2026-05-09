using CinemaApi.Data;
using CinemaApi.DTOs.Hall;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// Використовуємо Primary Constructor для AppDbContext
public class CinemaHallsController(AppDbContext context) : ControllerBase
{
    // GET: api/cinemahalls
    [HttpGet("/api/cinemahalls")]
    public async Task<ActionResult<List<HallResponseDto>>> GetAll()
    {
        // AsNoTracking пришвидшує роботу, бо ми тільки читаємо дані
        var halls = await context.Halls.AsNoTracking().ToListAsync();

        var response = halls.Select(h => new HallResponseDto
        {
            Id = h.Id,
            Name = h.Name,
            TotalRows = h.TotalRows,
            SeatsPerRow = h.SeatsPerRow,
            CinemaId = h.CinemaId // Додано для повної відповідності DTO
        }).ToList();

        return Ok(response);
    }

    // GET: api/cinemahalls/{id}
    [HttpGet("/api/cinemahalls/{id:int}")]
    public async Task<ActionResult<HallResponseDto>> GetById(int id)
    {
        var hall = await context.Halls.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

        if (hall == null) return NotFound(new { message = $"Зал з ID {id} не знайдено." });

        return Ok(new HallResponseDto
        {
            Id = hall.Id,
            Name = hall.Name,
            TotalRows = hall.TotalRows,
            SeatsPerRow = hall.SeatsPerRow,
            CinemaId = hall.CinemaId // Додано
        });
    }

    //POST: api/admin/cinemahalls
    [HttpPost("/api/admin/cinemahalls")]
    public async Task<IActionResult> Create([FromBody] HallRequestDto dto)
    {
        var cinemaExists = await context.Cinemas.AnyAsync(c => c.Id == dto.CinemaId);
        if (!cinemaExists) return BadRequest(new { message = "Вказаний кінотеатр не знайдено." });

        var hall = new Hall
        {
            Name = dto.Name,
            TotalRows = dto.TotalRows,
            SeatsPerRow = dto.SeatsPerRow,
            CinemaId = dto.CinemaId
        };

        context.Halls.Add(hall);
        await context.SaveChangesAsync();

        var response = new HallResponseDto
        {
            Id = hall.Id,
            Name = hall.Name,
            TotalRows = hall.TotalRows,
            SeatsPerRow = hall.SeatsPerRow,
            CinemaId = hall.CinemaId
        };

        return CreatedAtAction(nameof(GetById), new { id = hall.Id }, response);
    }

    // PUT: api/admin/cinemahalls/{id}
    [HttpPut("/api/admin/cinemahalls/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] HallRequestDto dto)
    {
        var hall = await context.Halls.FindAsync(id);
        if (hall == null) return NotFound(new { message = "Зал не знайдено." });

        var cinemaExists = await context.Cinemas.AnyAsync(c => c.Id == dto.CinemaId);
        if (!cinemaExists) return BadRequest(new { message = "Вказаний кінотеатр не знайдено." });

        hall.Name = dto.Name;
        hall.TotalRows = dto.TotalRows;
        hall.SeatsPerRow = dto.SeatsPerRow;
        hall.CinemaId = dto.CinemaId;

        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/admin/cinemahalls/{id}
    [HttpDelete("/api/admin/cinemahalls/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var targetHall = await context.Halls.FindAsync(id);

        if (targetHall == null) return NotFound();

        context.Halls.Remove(targetHall);
        await context.SaveChangesAsync();

        return NoContent();
    }
}