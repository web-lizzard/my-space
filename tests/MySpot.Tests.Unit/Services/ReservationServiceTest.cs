using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using Shouldly;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{

    private static readonly Clock Clock = new();
    private readonly List<WeeklyParkingSpot> spots = new()
    {

       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(Clock.Current()), "P1"),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(Clock.Current()), "P2"),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(Clock.Current()), "P3"),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(Clock.Current()), "P4"),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(Clock.Current()), "P5")

    };
    [Fact]
    public void given_reservation__for_not_taken_date_add_reservation_shoould_succed()
    {
        var parkingSpot = spots.First();
        var command = new CreateReservation(
            parkingSpot.Id,
            Guid.NewGuid(),
            "John Doe",
            "XYZ123",
            DateTimeOffset.Now.AddMinutes(5)
        );

        var reservationId = _service.Create(command);

        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }

    private readonly ReservationService _service;

    public ReservationServiceTests()
    {
        _service = new ReservationService(spots);
    }

}