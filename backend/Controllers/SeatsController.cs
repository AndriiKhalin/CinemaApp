using CinemaApi.DTOs.Seat;
using CinemaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatsController(ISeatMapService seatMapService) : ControllerBase
{
    // GET /api/seats/session/{id}
    [HttpGet("/api/seats/session/{id:int}")]
    public async Task<ActionResult<SeatMapResponseDto>> GetSeats(int id)
    {
        var seatMap = await seatMapService.GetSeatMapAsync(id);
        if (seatMap == null)
            return NotFound(new { message = $"Session {id} not found." });

        return Ok(seatMap);
    }
}