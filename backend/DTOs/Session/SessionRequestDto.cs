using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Session;

public class SessionRequestDto
{
    [Required(ErrorMessage = "MovieId is required")]
    public int MovieId { get; set; }

    [Required(ErrorMessage = "HallId is required")]
    public int HallId { get; set; }

    [Required(ErrorMessage = "Start time is required")]
    public DateTime StartTime { get; set; }

    [Range(0.01, 10000, ErrorMessage = "Ticket price must be greater than 0")]
    public decimal TicketPrice { get; set; }
}