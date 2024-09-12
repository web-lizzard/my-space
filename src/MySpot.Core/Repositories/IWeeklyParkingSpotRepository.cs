using MySpot.Core.Entities;

namespace MySpot.Core.Repositories;


public interface IWeeklyParkingSpotRepository
{

    public WeeklyParkingSpot? FindById(Guid id);

    public IEnumerable<WeeklyParkingSpot> FindAll();

    public void Save(WeeklyParkingSpot spot);

    public void Update(WeeklyParkingSpot spot);

}