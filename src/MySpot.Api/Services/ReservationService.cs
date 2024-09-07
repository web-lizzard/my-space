using System.Collections.Generic;
using MySpot.Api.Models;

public class ReservationService
{

    private static readonly List<Reservation> reservations = [];
    private static readonly List<string> parkingSpotNames = [
        "P1", "P2", "P3", "P4", "P5"
    ];
    private static int id = 1;



    public Reservation? FindById(int id) => reservations.Find(reservation => reservation.Id == id);

    public IEnumerable<Reservation> FindAll() => reservations;

    public int? Create(Reservation reservation)
    {
        if (parkingSpotNames.All(name => name != reservation.ParkingSpotName))
        {
            return null;
        }
        reservation.Date = DateTime.UtcNow.AddDays(1).Date;
        var reservationsAlreadyExist = reservations.Any(name => name.ParkingSpotName == reservation.ParkingSpotName && name.Date.Date == reservation.Date.Date);

        if (reservationsAlreadyExist)
        {
            return null;
        }

        reservation.Id = id;
        id++;
        reservations.Add(reservation);
        return reservation.Id;
    }


    public bool Update(int id, Reservation reservation)
    {
        var existingReservation = reservations.Find(reservarion => reservarion.Id == id);

        if (existingReservation == null)
        {
            return false;
        }

        existingReservation.LicensePlate = reservation.LicensePlate;

        return true;
    }

    public bool Delete(int id)
    {
        var existingReservation = reservations.Find(reservarion => reservarion.Id == id);

        if (existingReservation == null)
        {
            return false;
        }

        reservations.Remove(existingReservation);

        return true;
    }


}
