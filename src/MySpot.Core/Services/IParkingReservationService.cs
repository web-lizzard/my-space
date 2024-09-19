using MySpot.Core.Entities;

namespace MySpot.Core.Services;
public interface IParkingReservationService
{
    void ReservedSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTite, WeeklyParkingSpot weeklyParkingSpotToReserve, Reservation reservation);
}

