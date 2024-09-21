using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;


public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = [];

    public WeeklyParkingSpot() { }
    public WeeklyParkingSpot(Guid id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }

    public ParkingSpotId Id { get; set; }
    public Week Week { get; set; }
    public string Name { get; set; }
    public IEnumerable<Reservation> Reservations => _reservations;


    internal void AddReservation(Reservation reservation, Date now)
    {
        var isInvalidDate = reservation.Date < Week.From ||
                            reservation.Date > Week.To ||
                            reservation.Date < now;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date.Value.Date);
        }

        var isAlreadyExists = _reservations.Any(r => r.Date == reservation.Date);

        if (isAlreadyExists)
        {
            throw new ParkingSpotAlreadyReserverdException(Name, reservation.Date);
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

