using MySpot.Application.DTO;
using MySpot.Core.Entities;

namespace MySpot.Infrastructure.DAL.Handlers;


internal static class Extension
{

    public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot spot)
    {
        return new()
        {
            Id = spot.Id.Id.ToString(),
            Name = spot.Name,
            Capacity = spot.Capacity,
            From = spot.Week.From.Value.DateTime,
            To = spot.Week.To.Value.DateTime,
            Reservations = spot.Reservations.Select(x => new ReservationDto
            {
                Id = x.Id,
                EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : string.Empty,
                Date = x.Date.Value.Date,
                Type = x is VehicleReservation ? "vehicle" : "cleaning"

            })
        };
    }

    public static UserDto AsDto(this User user)
    {
        return new()
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username
        };
    }
}