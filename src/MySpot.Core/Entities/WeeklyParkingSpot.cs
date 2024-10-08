using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;


public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = [];

    public const int MaxCapacity = 2;

    public ParkingSpotId Id { get; set; }
    public Week Week { get; set; }
    public string Name { get; set; }
    public Capacity Capacity { get; private set; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public WeeklyParkingSpot() { }

    public static WeeklyParkingSpot Create(Guid id, Week week, string name)
    {
        return new WeeklyParkingSpot(id, week, name, MaxCapacity);
    }
    private WeeklyParkingSpot(Guid id, Week week, string name, Capacity capacity)
    {
        Id = id;
        Week = week;
        Name = name;
        Capacity = capacity;
    }

    internal void AddReservation(Reservation reservation, Date now)
    {
        var isInvalidDate = reservation.Date < Week.From ||
                            reservation.Date > Week.To ||
                            reservation.Date < now;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date.Value.Date);
        }

        var dateCapacity = Reservations
                .Where(reservation => reservation.Date == reservation.Date)
                .Sum(reservation => reservation.Capacity);

        if (dateCapacity + reservation.Capacity > Capacity)
        {
            throw new ParkingSpotCapacityExceededException(Id);
        }

        _reservations.Add(reservation);
    }

    public void RemoveReservation(Reservation reservation)
    {
        _reservations.RemoveWhere(r => r.Id == reservation.Id);
    }

    public void RemoveReservations(IEnumerable<Reservation> reservations)
    {
        _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
    }



}
