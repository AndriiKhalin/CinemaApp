using CinemaApi.Data;
using CinemaApi.Interfaces;
using CinemaApi.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CinemaApi.Services;

public class EmailService(IConfiguration configuration, AppDbContext context) : IEmailService
{
    public async Task SendBookingConfirmationAsync(Ticket ticket)
    {
        var from = configuration["EmailSettings:From"] ?? "yourcinema@gmail.com";
        var password = configuration["EmailSettings:Password"] ?? "";
        var enabled = bool.Parse(configuration["EmailSettings:Enabled"] ?? "false");


        if (ticket.Session == null) await context.Entry(ticket).Reference(t => t.Session).LoadAsync();

        if (ticket.Session != null && ticket.Session.Movie == null)
            await context.Entry(ticket.Session).Reference(s => s.Movie).LoadAsync();

        if (!enabled)
        {
            Console.WriteLine(
                $"[EMAIL MOCK] To: {ticket.CustomerEmail} | Booking #{ticket.Id} | Movie: {ticket.Session?.Movie?.Title}");
            await Task.CompletedTask;
            return;
        }

        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress("Cinema", from));
        msg.To.Add(new MailboxAddress(ticket.CustomerName, ticket.CustomerEmail));
        msg.Subject = $"Your ticket — {ticket.Session?.Movie?.Title}";

        msg.Body = new TextPart("html")
        {
            Text = $@"
                    <h2>Booking #{ticket.Id} confirmed!</h2>
                    <p><b>Movie:</b> {ticket.Session?.Movie?.Title}</p>
                    <p><b>Time:</b> {ticket.Session?.StartTime:dd MMM yyyy HH:mm}</p>
                    <p><b>Seat:</b> Row {ticket.Row}, Number {ticket.SeatNumber}</p>
                    <p>Thank you for choosing our cinema!</p>"
        };

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(from, password);
            await smtp.SendAsync(msg);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}