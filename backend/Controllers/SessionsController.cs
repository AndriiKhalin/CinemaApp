using CinemaApi.Data;
using CinemaApi.DTOs.Seat;
using CinemaApi.DTOs.Session;
using CinemaApi.Interfaces;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISeatMapService _seatMapService;

        public SessionsController(AppDbContext context, ISeatMapService seatMapService)
        {
            _context = context;
            _seatMapService = seatMapService;
        }

        // GET /api/sessions
        [HttpGet]
        public async Task<ActionResult<List<SessionResponseDto>>> GetAll([FromQuery] int? movieId)
        {
            var query = _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Where(s => s.StartTime > DateTime.UtcNow)
                .AsNoTracking();

            if (movieId.HasValue)
            {
                query = query.Where(s => s.MovieId == movieId.Value);
            }

            var sessions = await query.ToListAsync();

            var response = sessions.Select(s => new SessionResponseDto
            {
                Id = s.Id,
                StartTime = s.StartTime,
                TicketPrice = s.TicketPrice,
                MovieTitle = s.Movie.Title,
                HallName = s.Hall.Name
            }).ToList();

            return Ok(response);
        }

        // GET /api/sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionResponseDto>> GetById(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null) return NotFound();

            return Ok(new SessionResponseDto
            {
                Id = session.Id,
                StartTime = session.StartTime,
                TicketPrice = session.TicketPrice,
                MovieTitle = session.Movie.Title,
                HallName = session.Hall.Name
            });
        }

        // GET /api/sessions/5/seats
        [HttpGet("{id}/seats")]
        public async Task<ActionResult<SeatMapResponseDto>> GetSeats(int id)
        {
            var seatMap = await _seatMapService.GetSeatMapAsync(id);

            if (seatMap == null) return NotFound("Session not found.");

            return Ok(seatMap);
        }

        // POST /api/sessions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionRequestDto dto)
        {
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == dto.MovieId);
            var hallExists = await _context.Halls.AnyAsync(h => h.Id == dto.HallId);

            if (!movieExists) return BadRequest("No movie with this ID was found.");
            if (!hallExists) return BadRequest("No movie with this ID was found.");

            var session = new Session
            {
                MovieId = dto.MovieId,
                HallId = dto.HallId,
                StartTime = dto.StartTime,
                TicketPrice = dto.TicketPrice
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
        }

        // DELETE /api/sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return NotFound();

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}