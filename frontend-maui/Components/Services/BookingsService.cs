using CinemaAdmin.Components.Models.Bookings;

namespace CinemaAdmin.Components.Services;

public class BookingsService
{
    private readonly HttpClient _http;

    public BookingsService(HttpClient http)
    {
        _http = http;
    }

    public Task<List<BookingModel>> GetBookingsAsync()
    {
        // TODO: GET /api/admin/bookings
        return Task.FromResult(new List<BookingModel>());
    }
}