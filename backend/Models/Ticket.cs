using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models;

public class Ticket
{
    public int Id { get; set; }

    [Required] public int SessionId { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public required string CustomerEmail { get; set; }

    [Required][MaxLength(200)] public required string CustomerName { get; set; }

    [Range(1, int.MaxValue)] public int Row { get; set; }

    [Range(1, int.MaxValue)] public int SeatNumber { get; set; }

    public bool IsPaid { get; set; }


    public required Session Session { get; set; }
}