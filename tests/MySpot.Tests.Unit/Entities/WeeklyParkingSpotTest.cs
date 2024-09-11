
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Shouldly;

namespace MySpot.Tests.Unit.Entities;

public class WeeklyParkingSpotTests


{

    private readonly WeeklyParkingSpot _weeklyParkingSpot;
    private readonly Date _currentDate;

    public WeeklyParkingSpotTests()
    {
        _currentDate = new Date(new DateTime(2024, 08, 10));
        _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_currentDate), "P1");
    }


    [Theory]
    [InlineData("2024-09-08")]
    [InlineData("2025-07-08")]

    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        var invalidDate = DateTime.Parse(dateString);
        var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "test name", "xyz123", new Date(invalidDate));

        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, new Date(_currentDate)));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_shoul_fail()
    {
        var reservationDate = _currentDate.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYS134", reservationDate);
        var nextReservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYS134", reservationDate);

        _weeklyParkingSpot.AddReservation(reservation, _currentDate);

        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, reservationDate));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ParkingSpotAlreadyReserverdException>();
    }

    [Fact]
    public void given_reservation__for_not_taken_date_add_reservation_shoould_succed()
    {
        var reservationDate = _currentDate.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYS134", reservationDate);

        _weeklyParkingSpot.AddReservation(reservation, _currentDate);

        _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        _weeklyParkingSpot.Reservations.ShouldContain(reservation);
    }


}