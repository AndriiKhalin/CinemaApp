using Microsoft.EntityFrameworkCore;
using CinemaApi.Models;

namespace CinemaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>()
            .HasIndex(t => new { t.SessionId, t.Row, t.SeatNumber })
            .IsUnique();

    }
}