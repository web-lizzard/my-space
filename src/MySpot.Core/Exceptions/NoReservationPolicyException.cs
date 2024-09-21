using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

public class NoReservationPolicyException(JobTitle title) : CustomException($"No Reservation Policy for {title.Value} has been found")
{
}