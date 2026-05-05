using CinemaApi.DTOs.Seat;

namespace CinemaApi.DTOs.Seat
{
    public class SeatMapResponseDto
    {
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }
        public List<SeatSelectionDto> AvailableSeats { get; set; } = new();
        public List<SeatSelectionDto> BookedSeats { get; set; } = new();
    }
}