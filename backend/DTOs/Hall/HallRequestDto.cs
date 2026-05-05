namespace CinemaApi.DTOs.Hall
{
    public class HallRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }
    }
}