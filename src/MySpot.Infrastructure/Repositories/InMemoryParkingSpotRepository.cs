using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Time;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.Repositories;


internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly IEnumerable<WeeklyParkingSpot> _weeklyParkingSpots;

    public InMemoryWeeklyParkingSpotRepository(IClock clock)
    {
        _weeklyParkingSpots = new List<WeeklyParkingSpot>  {
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1" ),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2" ),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3" ),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4" ),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5" )
    };
    }

    public Task<IEnumerable<WeeklyParkingSpot>> FindAll()
    {
        return Task.FromResult(_weeklyParkingSpots);
    }

    public Task<IEnumerable<WeeklyParkingSpot>> FindAllByWeek(Week week)
    {
        return Task.FromResult(_weeklyParkingSpots.Where(spot => spot.Week == week));
    }

    public Task<WeeklyParkingSpot?> FindById(Guid id)
    {
        return Task.FromResult(_weeklyParkingSpots.SingleOrDefault(spot => spot.Id == new ParkingSpotId(id)));
    }

    public Task<WeeklyParkingSpot?> FindWeeklySpotByReservation(ReservationId Id)
    {
        throw new NotImplementedException();
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