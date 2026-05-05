namespace CinemaApi.DTOs.Seat
{
    public class SeatSelectionDto
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
    }
}