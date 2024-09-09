using MySpot.Api.ValueObjects;

namespace MySpot.Api.Commands;

public record CreateReservation(Guid ParkingSpotId, Guid ReservationId, string EmployeeName, string LicensePlate, DateTimeOffset Date);
