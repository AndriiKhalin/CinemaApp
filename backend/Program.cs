using CinemaApi.Data;
using Microsoft.EntityFrameworkCore;
using CinemaApi.Interfaces;
using CinemaApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ISeatMapService, SeatMapService>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    if (app.Environment.IsDevelopment())
    {
        DbInitializer.Initialize(context);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();