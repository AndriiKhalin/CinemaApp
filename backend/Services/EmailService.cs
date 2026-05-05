using CinemaApi.Interfaces;
using CinemaApi.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CinemaApi.Services;

// install: dotnet add package MailKit
// Gmail setup: Google Account → Security → App passwords → generate one
public class EmailService : IEmailService
{
    private const string From = "yourcinema@gmail.com";
    private const string Password = "your-app-password";
    private const bool Enabled = false; // set true when credentials are ready

    public async Task SendBookingConfirmationAsync(Ticket ticket)
    {
        if (!Enabled)
        {
            Console.WriteLine($"[EMAIL MOCK] To: {ticket.CustomerEmail} | Booking #{ticket.Id}");
            await Task.CompletedTask;
            return;
        }

        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress("Cinema", From));
        msg.To.Add(new MailboxAddress(ticket.CustomerName, ticket.CustomerEmail));
        msg.Subject = $"Your ticket — {ticket.Session.Movie.Title}";
        msg.Body = new TextPart("html")
        {
            Text = $"<h2>Booking #{ticket.Id} confirmed!</h2>" +
                   $"<p>Movie: {ticket.Session.Movie.Title}</p>" +
                   $"<p>Time: {ticket.Session.StartTime:dd MMM yyyy HH:mm}</p>" +
                   $"<p>Total: {ticket.Session.TicketPrice} UAH</p>"
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(From, Password);
        await smtp.SendAsync(msg);
        await smtp.DisconnectAsync(true);
    }
}