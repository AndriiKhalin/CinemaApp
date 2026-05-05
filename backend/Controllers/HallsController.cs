using CinemaApi.Data;
using CinemaApi.DTOs.Hall;
using CinemaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController(AppDbContext context) : ControllerBase
    {
        // GET /api/halls
        [HttpGet]
        public async Task<List<HallResponseDto>> GetAll()
        {
            // REQUIREMENT: Return all halls.
            throw new NotImplementedException();
        }

        // GET /api/halls/2
        [HttpGet("{id}")]
        public async Task<HallResponseDto> GetById(int id)
        {
            // REQUIREMENT: Return one hall or 404.
            throw new NotImplementedException();
        }

        // POST /api/halls
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HallRequestDto request)
        {
            // REQUIREMENT: Add the hall to the DB and return 201 Created.
            throw new NotImplementedException();
        }

        // DELETE /api/halls/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // REQUIREMENT: Delete hall by id. Return 404 if not found, 204 on success.
            throw new NotImplementedException();
        }
    }
}
