using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.DAL.Handlers;

namespace MySpot.Application.Queries.Handlers;

internal sealed class GetWeeklyParkingSpotsHandler(MySpotDbContext context) : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
{
    private readonly MySpotDbContext _context = context;

    public async Task<IEnumerable<WeeklyParkingSpotDto>> Handle(GetWeeklyParkingSpots query)
    {
        var week = query.Date.HasValue ? new Week(new Date(query.Date.Value)) : null;
        var spots = await _context.WeeklyParkingSpots
            .Where(spot => week == null || spot.Week == week)
            .Include(spot => spot.Reservations)
            .AsNoTracking()
            .ToListAsync();

        return spots.Select(spot => spot.AsDto());
    }


}