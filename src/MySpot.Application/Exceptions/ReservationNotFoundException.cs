using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Exceptions;

public class ReservationNotFoundException(ReservationId id) : CustomException($"Reservation with ID {id} was not found")
{
}
