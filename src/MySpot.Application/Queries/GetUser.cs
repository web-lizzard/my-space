using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Queries;

public class GetUser(Guid userId) : IQuery<UserDto>
{
    public UserId UserId { get; set; } = userId;
}

