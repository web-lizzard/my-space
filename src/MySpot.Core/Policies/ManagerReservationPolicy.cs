
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class ManagerReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle title) => title == JobTitle.Manager;
    public bool CanReserved(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
                                .SelectMany(x => x.Reservations)
                                .OfType<VehicleReservation>()
                                .Count(r => r.EmployeeName == employeeName);

        return totalEmployeeReservations <= 4;
    }
}