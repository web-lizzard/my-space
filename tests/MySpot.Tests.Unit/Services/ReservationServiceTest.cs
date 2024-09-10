using MySpot.Api.Commands;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Tests.Unit.Shared;
using Shouldly;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{

    private static readonly IClock Clock = new TestClock();
    private readonly IWeeklyParkingSpotRepository repository = new TestWeeklyRepository();
    [Fact]
    public void given_reservation__for_not_taken_date_add_reservation_shoould_succed()
    {
        var parkingSpot = repository.FindAll().First();
        var command = new CreateReservation(
            parkingSpot.Id,
            Guid.NewGuid(),
            "John Doe",
            "XYZ123",
            Clock.Current().AddMinutes(4)
        );

        var reservationId = _service.Create(command);

        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }

    private readonly ReservationService _service;

    public ReservationServiceTests()
    {
        _service = new ReservationService(Clock, repository);
    }

}