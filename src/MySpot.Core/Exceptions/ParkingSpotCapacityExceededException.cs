using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions;
internal class ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId) : CustomException($"Parking Spot with Id {parkingSpotId} exceeds reservation capacity")
{
}
