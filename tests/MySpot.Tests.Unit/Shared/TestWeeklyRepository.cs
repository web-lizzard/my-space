
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Time;
using MySpot.Core.ValueObjects;
using MySpot.Tests.Unit.Shared;

class TestWeeklyRepository : IWeeklyParkingSpotRepository
{

    private static IClock clock = new TestClock();
    private readonly IEnumerable<WeeklyParkingSpot> spots = new List<WeeklyParkingSpot>
    {
         WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1" ),
    };
    public Task<IEnumerable<WeeklyParkingSpot>> FindAll()
    {
        return Task.FromResult(spots);
    }

    public Task<IEnumerable<WeeklyParkingSpot>> FindAllByWeek(Week week)
    {
        return Task.FromResult(spots);
    }

    public Task<WeeklyParkingSpot?> FindById(Guid id)
    {
        return Task.FromResult(spots.FirstOrDefault());
    }

    public Task Save(WeeklyParkingSpot spot)
    {
        return Task.CompletedTask;
    }

    public Task Update(WeeklyParkingSpot spot)
    {
        return Task.CompletedTask;
    }

}