using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.DAL;

namespace MySpot.Infrastructure.Repositories;
internal sealed class SQLWeeklyParkingSpotRepository(MySpotDbContext dbContext) : IWeeklyParkingSpotRepository
{
    private readonly MySpotDbContext _dbContext = dbContext;

    public IEnumerable<WeeklyParkingSpot> FindAll() => _dbContext.WeeklyParkingSpots.Include(spot => spot.Reservations).ToList();

    public WeeklyParkingSpot? FindById(Guid id)
    {
        return _dbContext.WeeklyParkingSpots.Include(spot => spot.Reservations).SingleOrDefault(spot => spot.Id == new ParkingSpotId(id));
    }

    public void Save(WeeklyParkingSpot spot)
    {
        _dbContext.Add(spot);
        _dbContext.SaveChanges();
    }

    public void Update(WeeklyParkingSpot spot)
    {
        _dbContext.Update(spot);
        _dbContext.SaveChanges();
    }

}