
using MySpot.Core.Time;

namespace MySpot.Infrastructure.Time;

internal class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}
