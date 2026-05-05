using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models;

public class Booking
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;
    public string CustomerEmail { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<BookedSeat> BookedSeats { get; set; } = new();
}