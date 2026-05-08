using CinemaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecommendationsController(IRecommendationService recommendationService) : ControllerBase
{
    // GET /api/recommendations/{genre}
    [HttpGet("{genre}")]
    public async Task<IActionResult> GetByGenre(string genre, CancellationToken ct)
    {
        try
        {
            var json = await recommendationService.GetByGenreAsync(genre, ct);
            return Ok(json);
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, new { message = "Recommendation service unavailable." });
        }
    }
}