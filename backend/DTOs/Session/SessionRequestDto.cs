namespace CinemaApi.DTOs.Session;

public record SessionRequestDto(
    int MovieId,
    int HallId,
    DateTime StartTime,
    decimal TicketPrice
);