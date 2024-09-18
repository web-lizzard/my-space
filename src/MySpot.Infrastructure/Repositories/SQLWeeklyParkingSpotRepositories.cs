using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.DAL;

namespace MySpot.Infrastructure.Repositories;
internal sealed class SQLWeeklyParkingSpotRepository(MySpotDbContext dbContext) : IWeeklyParkingSpotRepository
{
    private readonly MySpotDbContext _dbContext = dbContext;

    async public Task<IEnumerable<WeeklyParkingSpot>> FindAll()
    {
        var list = await _dbContext.WeeklyParkingSpots.Include(spot => spot.Reservations).ToListAsync();
        return list.AsEnumerable();
    }

    public Task<WeeklyParkingSpot?> FindById(Guid id)
    {
        return _dbContext.WeeklyParkingSpots.Include(spot => spot.Reservations).SingleOrDefaultAsync(spot => spot.Id == new ParkingSpotId(id));
    }

    async public Task Save(WeeklyParkingSpot spot)
    {
        await _dbContext.AddAsync(spot);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(WeeklyParkingSpot spot)
    {
        _dbContext.Update(spot);
        await _dbContext.SaveChangesAsync();
    }

}