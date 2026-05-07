namespace CinemaApi.DTOs.Session
{
    public class SessionResponseDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
    }
}