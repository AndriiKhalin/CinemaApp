using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Seat;

public class SeatRequestDto
{
    [Range(1, int.MaxValue)] public int Row { get; set; }

    [Range(1, int.MaxValue)] public int Number { get; set; }
}