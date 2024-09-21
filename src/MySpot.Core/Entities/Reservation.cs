using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public abstract class Reservation

{
    protected Reservation() { }
    public ReservationId Id { get; private set; }
    public ParkingSpotId ParkingSpotId { get; private set; }

    public Capacity Capacity { get; private set; }

    public Date Date { get; private set; }

    public Reservation(Guid id, Guid parkingSpotId, Date date, Capacity capacity)
    {
        Id = id;
        ParkingSpotId = parkingSpotId;
        Date = date;
        Capacity = capacity;
    }



}
