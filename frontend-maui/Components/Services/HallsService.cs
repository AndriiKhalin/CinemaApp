using CinemaAdmin.Components.Models.Halls;

namespace CinemaAdmin.Components.Services;

public class HallsService
{
    private readonly HttpClient _http;

    public HallsService(HttpClient http)
    {
        _http = http;
    }

    public Task<List<HallModel>> GetHallsAsync()
    {
        // TODO: GET /api/cinemahalls
        return Task.FromResult(new List<HallModel>());
    }

    public Task<HallModel?> GetHallAsync(int id)
    {
        // TODO: GET /api/cinemahalls/{id}
        return Task.FromResult<HallModel?>(null);
    }

    public Task CreateHallAsync(HallUpsertModel model)
    {
        // TODO: POST /api/admin/cinemahalls
        return Task.CompletedTask;
    }

    public Task UpdateHallAsync(int id, HallUpsertModel model)
    {
        // TODO: PUT /api/admin/cinemahalls/{id}
        return Task.CompletedTask;
    }

    public Task DeleteHallAsync(int id)
    {
        // TODO: DELETE /api/admin/cinemahalls/{id}
        return Task.CompletedTask;
    }
}