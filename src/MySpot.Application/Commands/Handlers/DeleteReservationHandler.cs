
using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class DeleteReservationHandler(IWeeklyParkingSpotRepository repository) : ICommandHandler<DeleteReservation>
{
    private readonly IWeeklyParkingSpotRepository _repository = repository;

    public async Task Handle(DeleteReservation command)
    {
        var parkingSpot = await _repository.FindWeeklySpotByReservation(command.Id) ?? throw new WeeklyParkingSpotByReservaionNotFoundExceptions(command.Id);
        var reservation = parkingSpot.Reservations.SingleOrDefault(reservation => reservation.Id == new ReservationId(command.Id)) ?? throw new ReservationNotFoundException(command.Id);
        parkingSpot.RemoveReservation(reservation);
        await _repository.Update(parkingSpot);
    }


}