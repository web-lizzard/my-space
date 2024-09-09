using MySpot.Api.ValueObjects;

namespace MySpot.Api.Exceptions;
public sealed class ParkingSpotAlreadyReserverdException : CustomException
{
    public ParkingSpotAlreadyReserverdException(string name, Date date) : base($"Parking spot {name} is already reserved at {date:d}")
    {
    }


}