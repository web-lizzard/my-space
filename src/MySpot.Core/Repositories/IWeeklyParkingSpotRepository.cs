using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories;


public interface IWeeklyParkingSpotRepository
{

    public Task<WeeklyParkingSpot?> FindById(Guid id);

    public Task<IEnumerable<WeeklyParkingSpot>> FindAllByWeek(Week week)
    => throw new NotImplementedException();

    public Task<IEnumerable<WeeklyParkingSpot>> FindAll();

    Task Save(WeeklyParkingSpot spot);

    public Task Update(WeeklyParkingSpot spot);

}