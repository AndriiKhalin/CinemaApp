using CinemaAdmin.Components.Models.Cinemas;

namespace CinemaAdmin.Components.Services;

public class CinemasService
{
    private readonly HttpClient _http;

    public CinemasService(HttpClient http)
    {
        _http = http;
    }

    public Task<List<CinemaModel>> GetCinemasAsync()
    {
        // TODO: GET /api/cinemas
        return Task.FromResult(new List<CinemaModel>());
    }

    public Task<CinemaModel?> GetCinemaAsync(int id)
    {
        // TODO: GET /api/cinemas/{id}
        return Task.FromResult<CinemaModel?>(null);
    }

    public Task CreateCinemaAsync(CinemaUpsertModel model)
    {
        // TODO: POST /api/admin/cinemas
        return Task.CompletedTask;
    }

    public Task UpdateCinemaAsync(int id, CinemaUpsertModel model)
    {
        // TODO: PUT /api/admin/cinemas/{id}
        return Task.CompletedTask;
    }

    public Task DeleteCinemaAsync(int id)
    {
        // TODO: DELETE /api/admin/cinemas/{id}
        return Task.CompletedTask;
    }
}