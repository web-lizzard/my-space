namespace MySpot.Infrastructure.DAL;

using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;

internal sealed class MySpotDbContext(DbContextOptions<MySpotDbContext> dbContextOptions) : DbContext(dbContextOptions)
{

    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

}