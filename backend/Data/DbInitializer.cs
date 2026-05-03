using CinemaApi.Models;

namespace CinemaApi.Data;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // If we already seeded at least one cinema, skip.
        if (context.Cinemas.Any())
            return;

        var cinema = new Cinema
        {
            Name = "CinemaApp Kyiv Center",
            Address = "Kyiv, Main Street 1"
        };

        var hall = new Hall
        {
            Name = "Hall 1",
            TotalRows = 8,
            SeatsPerRow = 12,
            Cinema = cinema
        };

        var movies = new List<Movie>
        {
            new Movie
            {
                Title = "Inception",
                Genre = "Sci-Fi",
                Duration = 148,
                PosterUrl = "https://example.com/inception.jpg"
            },
            new Movie
            {
                Title = "Interstellar",
                Genre = "Sci-Fi",
                Duration = 169,
                PosterUrl = "https://example.com/interstellar.jpg"
            },
            new Movie
            {
                Title = "The Dark Knight",
                Genre = "Action",
                Duration = 152,
                PosterUrl = "https://example.com/darkknight.jpg"
            }
        };

        // Sessions for today and tomorrow
        var today = DateTime.Today;
        var sessions = new List<Session>
        {
            new Session { Movie = movies[0], Hall = hall, StartTime = today.AddHours(14), TicketPrice = 200 },
            new Session { Movie = movies[1], Hall = hall, StartTime = today.AddHours(17), TicketPrice = 220 },
            new Session { Movie = movies[2], Hall = hall, StartTime = today.AddHours(20), TicketPrice = 210 },

            new Session { Movie = movies[0], Hall = hall, StartTime = today.AddDays(1).AddHours(16), TicketPrice = 200 },
            new Session { Movie = movies[1], Hall = hall, StartTime = today.AddDays(1).AddHours(19), TicketPrice = 220 }
        };

        context.Cinemas.Add(cinema);
        context.Halls.Add(hall);
        context.Movies.AddRange(movies);
        context.Sessions.AddRange(sessions);

        context.SaveChanges();
    }
}