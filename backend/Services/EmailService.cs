using System.Text.Encodings.Web;
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
        var enabled = bool.TryParse(configuration["EmailSettings:Enabled"], out var isEnabled) && isEnabled;
        var host = configuration["EmailSettings:Host"] ?? "smtp.gmail.com";
        var port = int.TryParse(configuration["EmailSettings:Port"], out var p) ? p : 587;


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

        var movieTitle = HtmlEncoder.Default.Encode(ticket.Session?.Movie?.Title ?? "Unknown");
        var timeText = HtmlEncoder.Default.Encode(ticket.Session?.StartTime.ToString("dd MMM yyyy HH:mm") ?? "");
        var rowText = HtmlEncoder.Default.Encode(ticket.Row.ToString());
        var seatText = HtmlEncoder.Default.Encode(ticket.SeatNumber.ToString());
        msg.Subject = $"Your ticket — {movieTitle}";

        msg.Body = new TextPart("html")
        {
            Text = $@"
                    <h2>Booking #{ticket.Id} confirmed!</h2>
                    <p><b>Movie:</b> {movieTitle}</p>
                    <p><b>Time:</b> {timeText}</p>
                    <p><b>Seat:</b> Row {rowText}, Number {seatText}</p>
                    <p>Thank you for choosing our cinema!</p>"
        };

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(from, password);
            await smtp.SendAsync(msg);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}