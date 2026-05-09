using CinemaApi.Data;
using CinemaApi.DTOs.Cinema;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CinemasController(AppDbContext context) : ControllerBase
{
    [HttpGet("/api/cinemas")]
    public async Task<ActionResult<List<CinemaResponseDto>>> GetAll()
    {
        var cinemas = await context.Cinemas
            .AsNoTracking()
            .Select(c => new CinemaResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address
            })
            .ToListAsync();

        return Ok(cinemas);
    }

    [HttpGet("/api/cinemas/{id:int}")]
    public async Task<ActionResult<CinemaResponseDto>> GetById(int id)
    {
        var cinema = await context.Cinemas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cinema == null) return NotFound(new { message = $"Кінотеатр з ID {id} не знайдено." });

        return Ok(new CinemaResponseDto
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Address = cinema.Address
        });
    }

    [HttpPost("/api/admin/cinemas")]
    public async Task<IActionResult> Create([FromBody] CinemaRequestDto dto)
    {
        var cinema = new Cinema
        {
            Name = dto.Name,
            Address = dto.Address
        };

        context.Cinemas.Add(cinema);
        await context.SaveChangesAsync();

        var response = new CinemaResponseDto
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Address = cinema.Address
        };

        return CreatedAtAction(nameof(GetById), new { id = cinema.Id }, response);
    }

    [HttpPut("/api/admin/cinemas/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CinemaRequestDto dto)
    {
        var cinema = await context.Cinemas.FindAsync(id);

        if (cinema == null) return NotFound(new { message = "Кінотеатр не знайдено." });

        cinema.Name = dto.Name;
        cinema.Address = dto.Address;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("/api/admin/cinemas/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cinema = await context.Cinemas.FindAsync(id);
        if (cinema == null) return NotFound();

        context.Cinemas.Remove(cinema);
        await context.SaveChangesAsync();

        return NoContent();
    }
}