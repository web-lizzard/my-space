using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForCleaningHandler(
    IWeeklyParkingSpotRepository repository, IParkingReservationService reservationService) : ICommandHandler<ReserveParkingSpotForCleaning>
{
    private readonly IWeeklyParkingSpotRepository _repository = repository;
    private readonly IParkingReservationService _reservationService = reservationService;

    public async Task Handle(ReserveParkingSpotForCleaning command)
    {
        var date = new Date(command.Date);
        var allParkingSpots = await _repository.FindAllByWeek(new Week(date));
        _reservationService.ReserveParkingForCleaning(allParkingSpots, date);

        foreach (var spot in allParkingSpots)
        {
            await _repository.Update(spot);
        }
    }
}