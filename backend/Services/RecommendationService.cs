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

    public async Task<JsonElement> GetByGenreAsync(string genre, CancellationToken ct)
    {
        var url = $"{_settings.BaseUrl.TrimEnd('/')}/recommendations/{Uri.EscapeDataString(genre)}";

        var response = await httpClient.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(ct);
        using var json = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

        return json.RootElement.Clone();
    }
}