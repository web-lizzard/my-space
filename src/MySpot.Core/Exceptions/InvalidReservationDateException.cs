namespace MySpot.Core.Exceptions;

public sealed class InvalidReservationDateException : CustomException
{
    public InvalidReservationDateException(DateTime date) : base($"Reservation date {date.ToShortDateString()} is invalid")
    {
    }
}