using MySpot.Application.Time;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.Repositories;


internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly IEnumerable<WeeklyParkingSpot> _weeklyParkingSpots;

    public InMemoryWeeklyParkingSpotRepository(IClock clock)
    {
        _weeklyParkingSpots = new List<WeeklyParkingSpot>  {
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1" ),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2" ),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3" ),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4" ),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5" )
    };
    }

    public IEnumerable<WeeklyParkingSpot> FindAll()
    {
        return _weeklyParkingSpots;
    }

    public WeeklyParkingSpot? FindById(Guid id)
    {
        return _weeklyParkingSpots.SingleOrDefault(spot => spot.Id == new ParkingSpotId(id));
    }
}