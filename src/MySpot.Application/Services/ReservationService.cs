using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.Time;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public class ReservationService(IClock clock, IWeeklyParkingSpotRepository repository, IParkingReservationService reservationService) : IReservationService
{

    private readonly IClock _clock = clock;
    private readonly IWeeklyParkingSpotRepository _repository = repository;
    private readonly IParkingReservationService _reservationService = reservationService;

    async public Task<ReservationDto?> FindById(Guid id)
    {
        var spots = await FindAllWeekly();
        return spots.SingleOrDefault(reservation => reservation.Id == id);

    }

    async public Task<IEnumerable<ReservationDto>> FindAllWeekly()
    {
        var spots = await _repository.FindAll();
        return spots.SelectMany(spot => spot.Reservations).Select(reservation => new ReservationDto
        {
            Id = reservation.Id,
            ParkingSpotId = reservation.ParkingSpotId,
            Date = reservation.Date,
            LicensePlate = reservation.LicensePlate

        });
    }

    async public Task<Guid?> Create(CreateReservation command)
    {
        var week = new Week(_clock.Current());
        var allParkingSpots = await _repository.FindAllByWeek(week);
        var parkingSpotToReserved = allParkingSpots.SingleOrDefault(x => x.Id == new ParkingSpotId(command.ParkingSpotId));


        if (parkingSpotToReserved is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Date);
        _reservationService.ReservedSpotForVehicle(allParkingSpots, JobTitle.Employee, parkingSpotToReserved, reservation);
        await _repository.Update(parkingSpotToReserved);
        return reservation.Id;
    }


    async public Task<bool> Update(UpdateReservationLicensePlate command)
    {
        var weeklyParkingSpot = await FindWeeklySpotByReservation(command.id);

        if (weeklyParkingSpot == null)
        {
            return false;
        }

        var reservartionToUpdate = weeklyParkingSpot.Reservations.SingleOrDefault(reservation => reservation.Id == new ReservationId(command.id));

        if (reservartionToUpdate == null || reservartionToUpdate.Date <= new Date(_clock.Current()))
        {
            return false;
        }

        reservartionToUpdate.ChangeLicensePlate(command.LicensePlate);
        await _repository.Update(weeklyParkingSpot);
        return true;
    }

    public async Task<bool> Delete(DeleteReservation command)
    {
        var parkingSpot = await FindWeeklySpotByReservation(command.id);

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
        await _repository.Update(parkingSpot);

        return true;
    }


    async private Task<WeeklyParkingSpot?> FindWeeklySpotByReservation(ReservationId reservationId) => (await _repository.FindAll()).SingleOrDefault(spot => spot.Reservations.Any(reservation => reservation.Id == reservationId));
}
