using CinemaApi.Interfaces;
using CinemaApi.Services;
using CinemaApi.Settings;

namespace CinemaApi;

public static class CinemaApiDI
{
    public static void AddCinemaApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // All Services
        services.AddScoped<ISeatMapService, SeatMapService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IEmailService, EmailService>();

        // RecommendationService
        services.AddHttpClient<IRecommendationService, RecommendationService>();
        services.Configure<RecommendationSettings>(
            configuration.GetSection("RecommendationService"));
    }
}