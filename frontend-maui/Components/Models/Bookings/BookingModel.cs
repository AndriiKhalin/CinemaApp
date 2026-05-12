namespace CinemaAdmin.Components.Models.Bookings;

public class BookingModel
{
    public int TicketId { get; set; }
    public int SessionId { get; set; }
    public int Row { get; set; }
    public int SeatNumber { get; set; }
    public DateTime PurchasedAt { get; set; }
}