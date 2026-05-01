using CinemaApi.Models;

namespace CinemaApi.Data;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Movies.Any()) return;

        var movie = new Movie
        {
            Title = "Inception",
            Genre = "Sci-Fi",
            Duration = 148,
            PosterUrl = "https://example.com/inception.jpg"
        };

        context.Movies.Add(movie);
        context.SaveChanges();
    }
}