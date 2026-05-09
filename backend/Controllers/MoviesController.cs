using CinemaApi.Data;
using CinemaApi.DTOs.Movie;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(AppDbContext context) : ControllerBase
{
    [HttpGet("/api/movies")]
    public async Task<ActionResult<List<MovieResponseDto>>> GetAll([FromQuery] string? genre)
    {
        var query = context.Movies.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(genre)) query = query.Where(m => m.Genre == genre);

        var movies = await query.Select(m => new MovieResponseDto
        {
            Id = m.Id,
            Title = m.Title,
            Genre = m.Genre,
            DurationMinutes = m.DurationMinutes,
            Description = m.Description,
            ImageUrl = m.PosterUrl // Використовуємо твій PosterUrl
        }).ToListAsync();

        return Ok(movies);
    }

    [HttpGet("/api/movies/{id:int}")]
    public async Task<ActionResult<MovieResponseDto>> GetById(int id)
    {
        var movie = await context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound(new { message = $"Фільм з ID {id} не знайдено." });

        return Ok(new MovieResponseDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            DurationMinutes = movie.DurationMinutes,
            Description = movie.Description,
            ImageUrl = movie.PosterUrl // Використовуємо твій PosterUrl
        });
    }

    [HttpPost("/api/admin/movies")]
    public async Task<IActionResult> Create([FromBody] MovieRequestDto dto)
    {
        var movie = new Movie
        {
            Title = dto.Title,
            Genre = dto.Genre,
            DurationMinutes = dto.DurationMinutes,
            Description = dto.Description,
            PosterUrl = dto.ImageUrl ?? string.Empty // Мапимо з DTO в модель
        };

        context.Movies.Add(movie);
        await context.SaveChangesAsync();

        var response = new MovieResponseDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            DurationMinutes = movie.DurationMinutes,
            Description = movie.Description,
            ImageUrl = movie.PosterUrl
        };

        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, response);
    }

    [HttpPut("/api/admin/movies/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MovieRequestDto dto)
    {
        var movie = await context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound(new { message = "Фільм не знайдено." });

        movie.Title = dto.Title;
        movie.Genre = dto.Genre;
        movie.DurationMinutes = dto.DurationMinutes;
        movie.Description = dto.Description;
        movie.PosterUrl = dto.ImageUrl ?? string.Empty;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("/api/admin/movies/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await context.Movies.FindAsync(id);

        if (movie == null) return NotFound();

        context.Movies.Remove(movie);
        await context.SaveChangesAsync();
        return NoContent();
    }
}