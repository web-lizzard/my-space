using MySpot.Core.Entities;

namespace MySpot.Core.Repositories;


public interface IWeeklyParkingSpotRepository
{

    public Task<WeeklyParkingSpot?> FindById(Guid id);

    public Task<IEnumerable<WeeklyParkingSpot>> FindAll();

    Task Save(WeeklyParkingSpot spot);

    public Task Update(WeeklyParkingSpot spot);

}