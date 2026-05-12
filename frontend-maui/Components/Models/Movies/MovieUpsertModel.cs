namespace CinemaAdmin.Components.Models.Movies;

public class MovieUpsertModel
{
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public int DurationMinutes { get; set; }
    public string Description { get; set; } = "";
    public string ImageUrl { get; set; } = "";
}