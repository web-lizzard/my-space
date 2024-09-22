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
        return await _dbContext.WeeklyParkingSpots
            .Include(spot => spot.Reservations)
            .ToListAsync();
    }

    async public Task<IEnumerable<WeeklyParkingSpot>> FindAllByWeek(Week week)
    {
        return await _dbContext.WeeklyParkingSpots
            .Include(spot => spot.Reservations)
            .Where(spot => spot.Week == week)
            .ToListAsync();
    }

    public Task<WeeklyParkingSpot?> FindById(Guid id)
    {
        return _dbContext.WeeklyParkingSpots
            .Include(spot => spot.Reservations)
            .SingleOrDefaultAsync(spot => spot.Id == new ParkingSpotId(id));
    }

    public Task<WeeklyParkingSpot?> FindWeeklySpotByReservation(ReservationId id)
    {

        // (await _repository.FindAll()).SingleOrDefault(spot => spot.Reservations.Any(reservation => reservation.Id == reservationId));
        return _dbContext.WeeklyParkingSpots.Include(spot => spot.Reservations)
                                            .Where(spot => spot.Reservations.Any(reservation => reservation.Id == id))
                                            .FirstOrDefaultAsync();
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