using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public RecommendationsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET /api/recommendations/{genre}
        [HttpGet("{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            try
            {

                var client = _httpClientFactory.CreateClient();


                var pythonServiceUrl = $"http://localhost:8000/recommendations/{genre}";

                var response = await client.GetAsync(pythonServiceUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var recommendations = JsonSerializer.Deserialize<object>(content);
                    return Ok(recommendations);
                }

                return StatusCode((int)response.StatusCode, "Python service is unavailable");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}