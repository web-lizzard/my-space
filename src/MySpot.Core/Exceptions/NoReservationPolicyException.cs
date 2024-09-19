using MySpot.Core.Exceptions;

public class NoReservationPolicyException(JobTitle title) : CustomException($"No Reservation Policy for {title.Value} has been found")
{
}