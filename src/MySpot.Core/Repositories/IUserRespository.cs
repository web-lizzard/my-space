using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories;
public interface IUserRepository
{
    public Task<User?> FindByEmail(Email email);

    public Task<User?> FindByName(Username name);

    public Task Save(User user);
}