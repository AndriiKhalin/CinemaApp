using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Session;

public class SessionRequestDto
{
    [Required(ErrorMessage = "MovieId is required")]
    [Range(1, int.MaxValue)]
    public int? MovieId { get; set; }

    [Required(ErrorMessage = "HallId is required")]
    [Range(1, int.MaxValue)]
    public int? HallId { get; set; }

    [Required(ErrorMessage = "Start time is required")]
    public DateTime? StartTime { get; set; }

    [Range(0.01, 10000, ErrorMessage = "Ticket price must be greater than 0")]
    public decimal TicketPrice { get; set; }
}