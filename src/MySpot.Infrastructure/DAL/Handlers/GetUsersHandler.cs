using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Handlers;


internal class GetWeekHandler(MySpotDbContext dbContext) : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly MySpotDbContext _dbContext = dbContext;

    async public Task<IEnumerable<UserDto>> Handle(GetUsers query)
    {
        var users = await _dbContext.Users.AsNoTracking().ToListAsync();

        return users.Select(user => user.AsDto());
    }
}
