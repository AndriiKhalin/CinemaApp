using System.Text.Json;
using CinemaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecommendationsController(IRecommendationService recommendationService) : ControllerBase
{
    // GET /api/recommendations/{genre}
    [HttpGet("/api/recommendations/{genre}")]
    public async Task<IActionResult> GetByGenre(string genre, CancellationToken ct)
    {
        try
        {
            var json = await recommendationService.GetByGenreAsync(genre, ct);
            return Ok(json);
        }
        catch (TaskCanceledException)
        {
            return StatusCode(503, new { message = "Recommendation service timeout." });
        }
        catch (JsonException)
        {
            return StatusCode(502, new { message = "Recommendation service returned invalid response." });
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, new { message = "Recommendation service unavailable." });
        }
    }
}