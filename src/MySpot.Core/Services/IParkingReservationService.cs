using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Services;
public interface IParkingReservationService
{
    void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTite, WeeklyParkingSpot weeklyParkingSpotToReserve, VehicleReservation reservation);
    void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkngSpots, Date date);
}

