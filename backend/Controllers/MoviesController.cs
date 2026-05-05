using CinemaApi.Data;
using CinemaApi.DTOs.Movie;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/movies
        [HttpGet]
        public async Task<ActionResult<List<MovieResponseDto>>> GetAll([FromQuery] string? genre)
        {
            var query = _context.Movies.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(m => m.Genre == genre);
            }

            var movies = await query.ToListAsync();

            var response = movies.Select(m => new MovieResponseDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                DurationMinutes = m.DurationMinutes,
                Description = m.Description
            }).ToList();

            return Ok(response);
        }

        // GET /api/movies/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieResponseDto>> GetById(int id)
        {
            var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                DurationMinutes = movie.DurationMinutes,
                Description = movie.Description
            });
        }

        // POST /api/movies
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieRequestDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Genre = dto.Genre,
                DurationMinutes = dto.DurationMinutes,
                Description = dto.Description
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
        }

        // PUT /api/movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MovieRequestDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = dto.Title;
            movie.Genre = dto.Genre;
            movie.DurationMinutes = dto.DurationMinutes;
            movie.Description = dto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}