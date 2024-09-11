using MySpot.Core.Exceptions;
namespace MySpot.Core.ValueObjects;
public sealed record ParkingSpotName(string value)
{
    public string Value { get; } = value ?? throw new InvalidParkingSpotName(value);

}