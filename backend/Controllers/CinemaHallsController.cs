using CinemaApi.Data;
using CinemaApi.DTOs.Hall;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaHallsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CinemaHallsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CinemaHalls
        [HttpGet]
        public async Task<ActionResult<List<HallResponseDto>>> GetAll()
        {
            var halls = await _context.Halls.ToListAsync();
            var response = halls.Select(h => new HallResponseDto
            {
                Id = h.Id,
                Name = h.Name,
                TotalRows = h.TotalRows,
                SeatsPerRow = h.SeatsPerRow
            }).ToList();

            return Ok(response);
        }

        // GET: api/CinemaHalls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HallResponseDto>> GetById(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null) return NotFound();

            return Ok(new HallResponseDto
            {
                Id = hall.Id,
                Name = hall.Name,
                TotalRows = hall.TotalRows,
                SeatsPerRow = hall.SeatsPerRow
            });
        }

        // DELETE: api/CinemaHalls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null) return NotFound();

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}