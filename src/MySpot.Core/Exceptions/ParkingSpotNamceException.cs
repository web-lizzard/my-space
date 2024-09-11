namespace MySpot.Core.Exceptions;

public sealed class InvalidParkingSpotName : CustomException
{
    public InvalidParkingSpotName(string? name) : base($"Invalid Employee Name! {name}")
    {
    }
}


