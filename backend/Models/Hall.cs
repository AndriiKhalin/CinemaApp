using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models;

public class Hall
{
    public int Id { get; set; }

    [Required] public int CinemaId { get; set; }

    [Required] [MaxLength(200)] public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue)] public int TotalRows { get; set; }

    [Range(1, int.MaxValue)] public int SeatsPerRow { get; set; }

    public Cinema? Cinema { get; set; }
    public List<Session> Sessions { get; set; } = new();
}