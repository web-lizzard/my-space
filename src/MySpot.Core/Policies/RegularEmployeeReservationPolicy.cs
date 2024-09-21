
using MySpot.Core.Entities;
using MySpot.Core.Time;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;
internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
{
    private readonly IClock _clock;

    public RegularEmployeeReservationPolicy(IClock clock)
    {
        _clock = clock;
    }
    public bool CanBeApplied(JobTitle title)
    {
        return title == JobTitle.Employee;
    }

    public bool CanReserved(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
                                .SelectMany(x => x.Reservations)
                                .OfType<VehicleReservation>()
                                .Count(r => r.EmployeeName == employeeName);

        return totalEmployeeReservations < 2 && _clock.Current().Hour > 4;
    }
}