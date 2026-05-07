using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController(IHttpClientFactory httpClientFactory) : ControllerBase
    {
        // GET /api/recommendations/{genre}
        [HttpGet("{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var pythonServiceUrl = $"http://localhost:8000/recommendations/{genre}";

                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.GetAsync(pythonServiceUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var recommendations = JsonSerializer.Deserialize<object>(content);
                    return Ok(recommendations);
                }

                return StatusCode((int)response.StatusCode, new
                {
                    message = "Recommendation service returned an error",
                    details = response.ReasonPhrase
                });
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, new { message = "Recommendation service (Python FastAPI) is currently unavailable. Check whether the script is running on port 8000." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error while retrieving recommendations.", error = ex.Message });
            }
        }
    }
}