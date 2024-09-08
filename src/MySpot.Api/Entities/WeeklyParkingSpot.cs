using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;


public class WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
{
    private readonly HashSet<Reservation> _reservations = [];

    public Guid Id { get; } = id;
    public DateTime From { get; } = from;
    public DateTime To { get; } = to;
    public string Name { get; } = name;
    public IEnumerable<Reservation> Reservations => _reservations;


    public void AddReservation(Reservation reservation)
    {
        var isInvalidDate = reservation.Date.Date < From ||
                            reservation.Date.Date > To ||
                            reservation.Date.Date < DateTime.UtcNow.Date;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date);
        }

        var isAlreadyExists = _reservations.Any(r => r.Date.Date == reservation.Date.Date);

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

