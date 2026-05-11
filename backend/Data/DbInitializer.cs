using CinemaApi.Models;

namespace CinemaApi.Data;

public static class DbInitializer
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
            new()
            {
                Title = "Inception",
                Genre = "Sci-Fi",
                DurationMinutes = 148,
                PosterUrl = "https://example.com/inception.jpg"
            },
            new()
            {
                Title = "Interstellar",
                Genre = "Sci-Fi",
                DurationMinutes = 169,
                PosterUrl = "https://example.com/interstellar.jpg"
            },
            new()
            {
                Title = "The Dark Knight",
                Genre = "Action",
                DurationMinutes = 152,
                PosterUrl = "https://example.com/darkknight.jpg"
            }
        };

        // Sessions for today and tomorrow
        var today = DateTime.Today;
        var sessions = new List<Session>
        {
            new() { Movie = movies[0], Hall = hall, StartTime = today.AddHours(14), TicketPrice = 200 },
            new() { Movie = movies[1], Hall = hall, StartTime = today.AddHours(17), TicketPrice = 220 },
            new() { Movie = movies[2], Hall = hall, StartTime = today.AddHours(20), TicketPrice = 210 },

            new() { Movie = movies[0], Hall = hall, StartTime = today.AddDays(1).AddHours(16), TicketPrice = 200 },
            new() { Movie = movies[1], Hall = hall, StartTime = today.AddDays(1).AddHours(19), TicketPrice = 220 }
        };

        context.Cinemas.Add(cinema);
        context.Halls.Add(hall);
        context.Movies.AddRange(movies);
        context.Sessions.AddRange(sessions);

        context.SaveChanges();
    }
}