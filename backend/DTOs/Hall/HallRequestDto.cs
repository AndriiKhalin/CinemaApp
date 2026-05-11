using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Hall;

public class HallRequestDto
{
    [Required][StringLength(100)] public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue)] public int TotalRows { get; set; }

    [Range(1, int.MaxValue)] public int SeatsPerRow { get; set; }

    [Range(1, int.MaxValue)] public int CinemaId { get; set; }
}