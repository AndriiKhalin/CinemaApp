namespace CinemaAdmin.Components.Models.Sessions;

public class SessionModel
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public decimal TicketPrice { get; set; }
    public string MovieTitle { get; set; } = "";
    public string HallName { get; set; } = "";
}