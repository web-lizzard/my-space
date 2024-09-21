using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Policies;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.Time;
using MySpot.Tests.Unit.Shared;
using Shouldly;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{

    private static readonly IClock Clock = new TestClock();
    private readonly IWeeklyParkingSpotRepository repository = new TestWeeklyRepository();
    [Fact]
    async public void given_reservation__for_not_taken_date_add_reservation_shoould_succed()
    {
        var parkingSpot = (await repository.FindAll()).First();
        var command = new ReserveParkingSpotForVehicle(
            parkingSpot.Id,
            Guid.NewGuid(),
            "John Doe",
            "XYZ123",
            Clock.Current().AddMinutes(4)
        );

        var reservationId = await _service.ReserveForVehicle(command);

        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }

    private readonly IReservationService _service;

    public ReservationServiceTests()
    {
        var policies = new List<IReservationPolicy>()
        {
            new RegularEmployeeReservationPolicy(Clock),
            new ManagerReservationPolicy(),
            new BossReservationPolicy()
        };
        var parkingSpotReservationService = new ParkingReservationService(policies, Clock);
        _service = new ReservationService(Clock, repository, parkingSpotReservationService);
    }

}