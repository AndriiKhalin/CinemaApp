namespace CinemaApi.DTOs.Booking;

public class BookingResponseDto
{
    public int TicketId { get; set; }
    public int SessionId { get; set; }
    public int Row { get; set; }
    public int SeatNumber { get; set; }
    public DateTime? PurchasedAt { get; set; }
}