
using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands;

public record UpdateReservationLicensePlateForVehicle(Guid Id, string LicensePlate) : ICommand;