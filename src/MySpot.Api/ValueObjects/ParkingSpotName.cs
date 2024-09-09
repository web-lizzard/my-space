using MySpot.Api.Exceptions;
namespace MySpot.Api.ValueObjects;
public sealed record ParkingSpotName(string value)
{
    public string Value { get; } = value ?? throw new InvalidParkingSpotName(value);

}