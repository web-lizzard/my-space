
using MySpot.Application.Time;

namespace MySpot.Infrastructure.Time;

internal class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}
