using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;

namespace MySpot.Api.Services;

public class ReservationService
{

    private static readonly List<WeeklyParkingSpot> weeklyParkingSpots = [
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P1" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P2" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P3" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P4" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P5" ),
    ];


    public ReservationDto? FindById(Guid id) => FindAllWeekly().SingleOrDefault(reservation => reservation.Id == id);

    public IEnumerable<ReservationDto> FindAllWeekly() => weeklyParkingSpots.SelectMany(spot => spot.Reservations).Select(reservation => new ReservationDto
    {
        Id = reservation.Id,
        ParkingSpotId = reservation.ParkingSpotId,
        Date = reservation.Date,
        LicensePlate = reservation.LicensePlate
    });

    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = weeklyParkingSpots.SingleOrDefault(spot => spot.Id == command.ParkingSpotId);

        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Date);

        weeklyParkingSpot.AddReservation(reservation);

        return reservation.Id;
    }


    public bool Update(UpdateReservationLicensePlate command)
    {
        var existingReservation = FindReservation(command.id);

        if (existingReservation == null)
        {
            return false;
        }

        existingReservation.ChangeLicensePlate(command.LicensePlate);

        return true;
    }

    public bool Delete(DeleteReservation command)
    {
        var parkingSpot = FindWeeklySpotByReservation(command.id);

        if (parkingSpot is null)
        {
            return false;
        }
        var reservation = parkingSpot.Reservations.SingleOrDefault(reservation => reservation.Id == command.id);
        if (reservation == null)
        {
            return false;
        }
        parkingSpot.RemoveReservation(reservation);

        return true;
    }


    private Reservation? FindReservation(Guid id) => weeklyParkingSpots.SelectMany(spot => spot.Reservations).SingleOrDefault(reservation => reservation.Id == id);
    private WeeklyParkingSpot? FindWeeklySpotByReservation(Guid reservationId) => weeklyParkingSpots.SingleOrDefault(spot => spot.Reservations.Any(reservation => reservation.Id == reservationId));
}
