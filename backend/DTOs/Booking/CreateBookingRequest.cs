using CinemaApi.DTOs.Seat;

namespace CinemaApi.DTOs.Booking
{
    public class CreateBookingRequest
    {
        public int SessionId { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<SeatDto> Seats { get; set; } = new();
    }
}