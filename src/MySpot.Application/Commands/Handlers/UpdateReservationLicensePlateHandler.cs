
using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class UpdateReservationLicensePlateHandler(IWeeklyParkingSpotRepository repository) : ICommandHandler<UpdateReservationLicensePlateForVehicle>
{
    private readonly IWeeklyParkingSpotRepository _repository = repository;

    public async Task Handle(UpdateReservationLicensePlateForVehicle command)
    {
        var parkingSpot = await _repository.FindWeeklySpotByReservation(command.Id)
                          ?? throw new WeeklyParkingSpotByReservaionNotFoundExceptions(command.Id);
        var reservation = parkingSpot.Reservations.Cast<VehicleReservation>().SingleOrDefault(reservation => reservation.Id == new ReservationId(command.Id))
                          ?? throw new ReservationNotFoundException(command.Id);
        reservation.ChangeLicensePlate(command.LicensePlate);
        await _repository.Update(parkingSpot);
    }
}