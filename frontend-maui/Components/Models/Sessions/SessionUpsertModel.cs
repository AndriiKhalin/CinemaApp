namespace CinemaAdmin.Components.Models.Sessions;

public class SessionUpsertModel
{
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal TicketPrice { get; set; }
}