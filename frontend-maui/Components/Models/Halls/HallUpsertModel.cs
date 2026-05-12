namespace CinemaAdmin.Components.Models.Halls;

public class HallUpsertModel
{
    public string Name { get; set; } = "";
    public int TotalRows { get; set; }
    public int SeatsPerRow { get; set; }
    public int CinemaId { get; set; }
}