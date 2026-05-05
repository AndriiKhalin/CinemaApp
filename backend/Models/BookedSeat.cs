namespace CinemaApi.Models;

public class BookedSeat
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
    public int SeatRow { get; set; }
    public int SeatNumber { get; set; }
}