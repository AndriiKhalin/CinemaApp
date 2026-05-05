namespace CinemaApi.DTOs.Hall
{
    public class HallResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }
    }
}