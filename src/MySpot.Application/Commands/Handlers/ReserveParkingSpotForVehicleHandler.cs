using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.Time;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForVehicleHandler(IWeeklyParkingSpotRepository repository, IParkingReservationService reservationService, IClock clock) : ICommandHandler<ReserveParkingSpotForVehicle>
{
    private readonly IWeeklyParkingSpotRepository _repository = repository;
    private readonly IParkingReservationService _reservationService = reservationService;

    private readonly IClock _clock = clock;

    public async Task Handle(ReserveParkingSpotForVehicle command)
    {
        var week = new Week(_clock.Current());
        var allParkingSpots = await _repository.FindAllByWeek(week);
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var parkingSpotToReserved = allParkingSpots.SingleOrDefault(spot => spot.Id == parkingSpotId) ?? throw new WeeklyParkingSpotNotFoundExceptions(parkingSpotId);
        var reservation = new VehicleReservation(
            command.ReservationId,
            command.ParkingSpotId,
            command.EmployeeName,
            command.LicensePlate,
            new Date(command.Date),
            command.Capacity);
        _reservationService.ReserveSpotForVehicle(allParkingSpots, JobTitle.Employee, parkingSpotToReserved, reservation);
        await _repository.Update(parkingSpotToReserved);

    }
}