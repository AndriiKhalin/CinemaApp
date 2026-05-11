using CinemaApi.Models;

namespace CinemaApi.Interfaces;

public interface IEmailService
{
    Task SendBookingConfirmationAsync(Ticket ticket);
}