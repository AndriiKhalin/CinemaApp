using CinemaApi.Data;
using CinemaApi.DTOs.Movie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(AppDbContext context) : ControllerBase
    {
        // GET /api/movies
        // GET /api/movies?genre=Comedy
        [HttpGet]
        public async Task<List<MovieResponseDto>> GetAll([FromQuery] string? genre)
        {
            // REQUIREMENT: Return all movies. If genre is provided, filter by it.
            // TODO: Start with _context.Movies.AsQueryable() and also if need use AsNoTracking() for better performance if not updating entities.
            // Apply .Where(m => m.Genre == genre) only if genre != null
            // Return List of MovieResponseDto (map from Movie entities).
            throw new NotImplementedException();
        }

        // GET /api/movies/5
        [HttpGet("{id:int}")]
        public async Task<MovieResponseDto> GetById(int id)
        {
            // REQUIREMENT: Return one movie or 404 if not found.
            // Hint: var movie = await _context.Movies.FindAsync(id);
            throw new NotImplementedException();
        }

        // POST /api/movies
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieRequestDto dto)
        {
            // REQUIREMENT: Create a new movie from the DTO and save it.
            // Map dto fields onto a new Movie object, then add and SaveChangesAsync.
            // Return CreatedAtAction pointing to GetById with the new movie.Id.
            throw new NotImplementedException();
        }

        // PUT /api/movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MovieRequestDto dto)
        {
            // REQUIREMENT: Find the movie, update its fields from dto, save.
            // Return NotFound if it doesn't exist, return NoContent on success.
            // Hint: find the movie first, then assign dto fields one by one, then SaveChangesAsync.
            throw new NotImplementedException();
        }

        // DELETE /api/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // REQUIREMENT: Remove a movie. Return 404 if not found, 204 on success.
            // Hint: _context.Movies.Remove(movie); await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
