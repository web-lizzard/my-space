
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class BossReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle title) => JobTitle.Boss == title;

    public bool CanReserved(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        return true;
    }
}