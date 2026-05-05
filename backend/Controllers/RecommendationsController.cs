using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        // TODO: Inject RecommendationService or HttpClient

        // GET /api/recommendations/{genre}
        [HttpGet("{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            // TODO: Call Python FastAPI service and return JSON
            throw new NotImplementedException();
        }
    }
}
