
using MySpot.Core.Time;

namespace MySpot.Tests.Unit.Shared;


class TestClock : IClock
{
    public DateTime Current() => new(2022, 08, 11);
}