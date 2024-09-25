using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Handlers;
internal sealed class GetUserHandler(MySpotDbContext dbContext) : IQueryHandler<GetUser, UserDto>
{
    private readonly MySpotDbContext _dbContext = dbContext;

    async public Task<UserDto> Handle(GetUser query)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(user => user.Id == query.UserId)
                   ?? throw new Exception("");

        return user.AsDto();
    }
}