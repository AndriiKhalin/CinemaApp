using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Movie;

public class MovieRequestDto
{
    [Required][StringLength(200)] public string Title { get; set; } = string.Empty;

    [Required][StringLength(100)] public string Genre { get; set; } = string.Empty;

    [Range(1, int.MaxValue)] public int DurationMinutes { get; set; }

    [StringLength(2000)] public string? Description { get; set; }

    [Url][StringLength(2048)] public string? ImageUrl { get; set; }
}