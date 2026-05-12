using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models;

public class Movie
{
    public int Id { get; set; }

    [Required][MaxLength(300)] public string Title { get; set; } = string.Empty;

    [Required][MaxLength(100)] public string Genre { get; set; } = string.Empty;

    [Range(1, int.MaxValue)] public int DurationMinutes { get; set; }

    [MaxLength(1000)] public string? Description { get; set; }

    [MaxLength(1000)] public string PosterUrl { get; set; } = string.Empty;

    public int? TmdbId { get; set; }

    public List<Session> Sessions { get; set; } = new();
}