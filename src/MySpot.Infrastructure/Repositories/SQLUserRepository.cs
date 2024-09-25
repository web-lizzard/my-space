using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.DAL;

namespace MySpot.Infrastructure.Repositories;


internal class SQLUserRepository(MySpotDbContext dbContext) : IUserRepository
{
    private readonly MySpotDbContext _dbContext = dbContext;

    public Task<User?> FindByEmail(Email email)
    {
        return _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);
    }

    public Task<User?> FindByName(Username name)
    {
        return _dbContext.Users.SingleOrDefaultAsync(user => user.Username == name);
    }

    public async Task Save(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}