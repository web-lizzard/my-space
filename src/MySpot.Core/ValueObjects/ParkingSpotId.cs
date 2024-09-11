namespace MySpot.Core.ValueObjects;
public record ParkingSpotId
{
    public Guid Id { get; }

    public ParkingSpotId(Guid id)
    {
        Id = id;
    }

    public static implicit operator Guid(ParkingSpotId parkingSpotId) => parkingSpotId.Id;
    public static implicit operator ParkingSpotId(Guid id) => new(id);
}