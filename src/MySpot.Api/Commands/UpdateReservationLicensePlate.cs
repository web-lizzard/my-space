using MySpot.Api.ValueObjects;

namespace MySpot.Api.Commands;

public record UpdateReservationLicensePlate(Guid id, string LicensePlate);