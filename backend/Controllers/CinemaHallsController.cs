using CinemaApi.Data;
using CinemaApi.DTOs.Hall;
using CinemaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Використовуємо Primary Constructor для AppDbContext
    public class CinemaHallsController(AppDbContext context) : ControllerBase
    {
        // GET: api/CinemaHalls
        [HttpGet]
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

        // GET: api/CinemaHalls/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HallResponseDto>> GetById(int id)
        {
            var hall = await context.Halls.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

            if (hall == null)
            {
                return NotFound(new { message = $"Зал з ID {id} не знайдено." });
            }

            return Ok(new HallResponseDto
            {
                Id = hall.Id,
                Name = hall.Name,
                TotalRows = hall.TotalRows,
                SeatsPerRow = hall.SeatsPerRow,
                CinemaId = hall.CinemaId // Додано
            });
        }

        // DELETE: api/CinemaHalls/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hall = await context.Movies.FindAsync(id); // Переконайся, що тут саме context.Halls

            // Виправлено на Halls
            var targetHall = await context.Halls.FindAsync(id);

            if (targetHall == null)
            {
                return NotFound();
            }

            context.Halls.Remove(targetHall);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}