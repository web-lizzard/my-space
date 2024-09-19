using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

public class CannotReserveParkingSpot(ParkingSpotId parkingSpotId) : CustomException($"Cannot reserve parkint spot on {parkingSpotId.Id}")
{
}