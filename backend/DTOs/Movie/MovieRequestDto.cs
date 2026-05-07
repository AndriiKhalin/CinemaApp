namespace CinemaApi.DTOs.Movie
{
    public class MovieRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public string? Description { get; set; }

        public string? ImageUrl { get; set; } 
    }
}