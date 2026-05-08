using System.Text.Json;

namespace CinemaApi.Interfaces;

public interface IRecommendationService
{
    Task<JsonElement> GetByGenreAsync(string genre, CancellationToken ct);
}