using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;


public class WeeklyParkingSpot(Guid id, Week week, string name)
{
    private readonly HashSet<Reservation> _reservations = [];

    public ParkingSpotId Id { get; } = id;
    public Week Week { get; } = week;
    public string Name { get; } = name;
    public IEnumerable<Reservation> Reservations => _reservations;


    public void AddReservation(Reservation reservation, Date now)
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
        _reservations.Remove(reservation);
    }



}

