using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MySpot.Application.Time;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL;


internal sealed class DatabaseInitializer(IClock clock, MySpotDbContext dbContext) : IHostedService
{
    private readonly IClock _clock = clock;
    private readonly MySpotDbContext _dbContext = dbContext;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _dbContext.Database.Migrate();
        var weeklyParkingSpots = _dbContext.WeeklyParkingSpots.ToList();
        if (!weeklyParkingSpots.Any())
        {
            var _weeklyParkingSpots = new List<WeeklyParkingSpot>()
            {
                new(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1" ),
                new(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2" ),
                new(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3" ),
                new(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4" ),
                new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5" )
            };

            _dbContext.AddRange(_weeklyParkingSpots);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}