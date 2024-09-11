
using MySpot.Application.Time;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using MySpot.Tests.Unit.Shared;

class TestWeeklyRepository : IWeeklyParkingSpotRepository
{

    private static IClock clock = new TestClock();
    private readonly IEnumerable<WeeklyParkingSpot> spots = new List<WeeklyParkingSpot>
    {
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1" ),
    };
    public IEnumerable<WeeklyParkingSpot> FindAll()
    {
        return spots;
    }

    public WeeklyParkingSpot? FindById(Guid id)
    {
        return spots.First();
    }
}