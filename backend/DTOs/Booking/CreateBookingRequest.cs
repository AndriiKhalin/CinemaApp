using System.ComponentModel.DataAnnotations;
using CinemaApi.DTOs.Seat;

namespace CinemaApi.DTOs.Booking;

public class CreateBookingRequest
{
    [Range(1, int.MaxValue)] public int SessionId { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required] [MaxLength(200)] public string CustomerName { get; set; } = string.Empty;

    public List<SeatRequestDto> Seats { get; set; } = new();
}