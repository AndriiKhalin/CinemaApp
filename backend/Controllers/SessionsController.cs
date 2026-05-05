using CinemaApi.Data;
using CinemaApi.DTOs.Seat;
using CinemaApi.DTOs.Session;
using CinemaApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController(AppDbContext context, ISeatMapService seatMapService) : ControllerBase
    {
        // GET /api/sessions
        // GET /api/sessions?movieId=3
        [HttpGet]
        public async Task<List<SessionResponseDto>> GetAll([FromQuery] int? movieId)
        {
            // REQUIREMENT: Return all sessions that start in the future.
            // If movieId is provided, filter to only sessions for that movie.
            // Include Hall and Movie in the result so the frontend has all the info.
            // Hint: .Include(s => s.Movie).Include(s => s.Hall)
            //       .Where(s => s.StartTime > DateTime.UtcNow)
            throw new NotImplementedException();
        }

        // GET /api/sessions/5
        [HttpGet("{id}")]
        public async Task<SessionResponseDto> GetById(int id)
        {
            // REQUIREMENT: Return one session with Movie and Hall included. 404 if not found.
            // Hint: use FirstOrDefaultAsync instead of FindAsync so you can chain Include.
            throw new NotImplementedException();
        }

        // GET /api/sessions/5/seats
        // Returns the seat grid: hall dimensions + list of already-booked seats.
        [HttpGet("{id}/seats")]
        public async Task<SeatDto> GetSeats(int id)
        {
            // call service to get the seat map for this session. The service will return null if the session doesn't exist, so return 404 in that case.
            throw new NotImplementedException();
        }

        // POST /api/sessions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionRequestDto dto)
        {
            // REQUIREMENT: Validate that MovieId and HallId exist in the DB before saving.
            // If either is not found, return BadRequest with a descriptive message.
            // Map dto → Session, add, SaveChangesAsync, return CreatedAtAction.
            // Hint: var movieExists = await _context.Movies.AnyAsync(m => m.Id == dto.MovieId);
            throw new NotImplementedException();
        }

        // DELETE /api/sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // REQUIREMENT: Delete the session. Return 404 if not found, 204 on success.
            throw new NotImplementedException();
        }
    }
}
