namespace CinemaApi.Models;

public class Cinema
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Address { get; set; }

    public List<Hall> Halls { get; set; } = new();
}