using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationService
{

    private static Clock _clock = new();

    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;


    public ReservationService(List<WeeklyParkingSpot> weeklyParkingSpots)
    {
        _weeklyParkingSpots = weeklyParkingSpots;
    }


    public ReservationDto? FindById(Guid id) => FindAllWeekly().SingleOrDefault(reservation => reservation.Id == id);

    public IEnumerable<ReservationDto> FindAllWeekly() => _weeklyParkingSpots.SelectMany(spot => spot.Reservations).Select(reservation => new ReservationDto
    {
        Id = reservation.Id,
        ParkingSpotId = reservation.ParkingSpotId,
        Date = reservation.Date,
        LicensePlate = reservation.LicensePlate
    });

    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(spot => spot.Id == new ParkingSpotId(command.ParkingSpotId));

        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Date);
        weeklyParkingSpot.AddReservation(reservation, _clock.Current());

        return reservation.Id;
    }


    public bool Update(UpdateReservationLicensePlate command)
    {
        var existingReservation = FindReservation(command.id);

        if (existingReservation == null)
        {
            return false;
        }

        if (existingReservation.Date <= _clock.Current())
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
        var reservation = parkingSpot.Reservations.SingleOrDefault(reservation => reservation.Id == new ReservationId(command.id));
        if (reservation == null)
        {
            return false;
        }
        parkingSpot.RemoveReservation(reservation);

        return true;
    }


    private Reservation? FindReservation(ReservationId id) => _weeklyParkingSpots.SelectMany(spot => spot.Reservations).SingleOrDefault(reservation => reservation.Id == id);
    private WeeklyParkingSpot? FindWeeklySpotByReservation(ReservationId reservationId) => _weeklyParkingSpots.SingleOrDefault(spot => spot.Reservations.Any(reservation => reservation.Id == reservationId));
}
