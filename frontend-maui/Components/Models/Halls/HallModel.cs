namespace CinemaAdmin.Components.Models.Halls;

public class HallModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int TotalRows { get; set; }
    public int SeatsPerRow { get; set; }
    public int CinemaId { get; set; }
}