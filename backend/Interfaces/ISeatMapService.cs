using CinemaApi.DTOs.Seat;

namespace CinemaApi.Interfaces;

public interface ISeatMapService
{
    Task<SeatMapResponseDto?> GetSeatMapAsync(int sessionId);
}