namespace MySpot.Core.ValueObjects;
public sealed record ReservationId
{
    public Guid Id { get; }

    public ReservationId(Guid id)
    {
        Id = id;
    }

    public static ReservationId Create()
    {
        return new ReservationId(Guid.NewGuid());
    }

    public static implicit operator Guid(ReservationId reservationId) => reservationId.Id;
    public static implicit operator ReservationId(Guid id) => new(id);

}