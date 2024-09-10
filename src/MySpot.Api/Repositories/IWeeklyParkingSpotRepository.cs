using MySpot.Api.Entities;

namespace MySpot.Api.Repositories;


public interface IWeeklyParkingSpotRepository
{

    public WeeklyParkingSpot? FindById(Guid id);

    public IEnumerable<WeeklyParkingSpot> FindAll();

}