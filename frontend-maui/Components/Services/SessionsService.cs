using CinemaAdmin.Components.Models.Sessions;

namespace CinemaAdmin.Components.Services;

public class SessionsService
{
    private readonly HttpClient _http;

    public SessionsService(HttpClient http)
    {
        _http = http;
    }

    public Task<List<SessionModel>> GetSessionsAsync(int? movieId = null)
    {
        // TODO: GET /api/sessions?movieId={id} (if movieId provided)
        return Task.FromResult(new List<SessionModel>());
    }

    public Task<SessionModel?> GetSessionAsync(int id)
    {
        // TODO: GET /api/sessions/{id}
        return Task.FromResult<SessionModel?>(null);
    }

    public Task CreateSessionAsync(SessionUpsertModel model)
    {
        // TODO: POST /api/admin/sessions
        return Task.CompletedTask;
    }

    public Task UpdateSessionAsync(int id, SessionUpsertModel model)
    {
        // TODO: PUT /api/admin/sessions/{id}
        return Task.CompletedTask;
    }

    public Task DeleteSessionAsync(int id)
    {
        // TODO: DELETE /api/admin/sessions/{id}
        return Task.CompletedTask;
    }
}