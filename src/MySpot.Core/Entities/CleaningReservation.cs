using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;
public class CleaningReservation : Reservation
{
    private CleaningReservation() { }
    public CleaningReservation(Guid id, Guid parkingSpotId, Date date) : base(id, parkingSpotId, date, 2)
    {
    }
}