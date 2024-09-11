using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions;
public sealed class ParkingSpotAlreadyReserverdException : CustomException
{
    public ParkingSpotAlreadyReserverdException(string name, Date date) : base($"Parking spot {name} is already reserved at {date:d}")
    {
    }


}