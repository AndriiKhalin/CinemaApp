using System.Text.Json;
using CinemaApi.Interfaces;
using CinemaApi.Settings;
using Microsoft.Extensions.Options;

namespace CinemaApi.Services;

public class RecommendationService(
    HttpClient httpClient,
    IOptions<RecommendationSettings> settings) : IRecommendationService
{
    private readonly RecommendationSettings _settings = settings.Value;

    // TODO: Use HttpClient to call Python service
    // Example: Task<string> GetByGenreAsync(string genre)
    // Don't forget realised also interface IRecommendationService
    public async Task<JsonElement> GetByGenreAsync(string genre, CancellationToken ct)
    {
        var url = $"{_settings.BaseUrl.TrimEnd('/')}/recommendations/{Uri.EscapeDataString(genre)}";

        var response = await httpClient.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync(ct);
        var json = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

        return json.RootElement.Clone();
    }
}