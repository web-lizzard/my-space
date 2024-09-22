using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Exceptions;

public class WeeklyParkingSpotByReservaionNotFoundExceptions(ReservationId id) : CustomException($"Weekly parking spot for reservation with {id} was not found")
{
}
