using CinemaAdmin.Components.Models.Movies;

namespace CinemaAdmin.Components.Services;

public class MoviesService
{
    private readonly HttpClient _http;

    public MoviesService(HttpClient http)
    {
        _http = http;
    }

    public Task<List<MovieModel>> GetMoviesAsync()
    {
        // TODO: GET /api/movies
        // TODO: return deserialized list
        return Task.FromResult(new List<MovieModel>());
    }

    public Task<MovieModel?> GetMovieAsync(int id)
    {
        // TODO: GET /api/movies/{id}
        return Task.FromResult<MovieModel?>(null);
    }

    public Task CreateMovieAsync(MovieUpsertModel model)
    {
        // TODO: POST /api/admin/movies
        return Task.CompletedTask;
    }

    public Task UpdateMovieAsync(int id, MovieUpsertModel model)
    {
        // TODO: PUT /api/admin/movies/{id}
        return Task.CompletedTask;
    }

    public Task DeleteMovieAsync(int id)
    {
        // TODO: DELETE /api/admin/movies/{id}
        return Task.CompletedTask;
    }
}