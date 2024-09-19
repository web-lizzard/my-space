using MySpot.Core.Entities;
using MySpot.Core.Policies;
using MySpot.Core.Time;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Services;

internal sealed class ParkingReservationService(IEnumerable<IReservationPolicy> policies, IClock clock) : IParkingReservationService
{
    private readonly IEnumerable<IReservationPolicy> _policies = policies;
    private readonly IClock _clock = clock;

    public void ReservedSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTite, WeeklyParkingSpot weeklyParkingSpotToReserve, Reservation reservation)
    {
        var parkingSpotId = weeklyParkingSpotToReserve.Id;
        var policy = _policies.SingleOrDefault(p => p.CanBeApplied(jobTite)) ?? throw new NoReservationPolicyException(jobTite);

        if (!policy.CanReserved(allParkingSpots, reservation.EmployeeName))
        {
            throw new CannotReserveParkingSpot(parkingSpotId);
        }

        weeklyParkingSpotToReserve.AddReservation(reservation, new Date(_clock.Current()));
    }
}

