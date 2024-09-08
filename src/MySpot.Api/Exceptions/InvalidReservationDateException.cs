namespace MySpot.Api.Exceptions;

internal sealed class InvalidReservationDateException : CustomException
{
    public InvalidReservationDateException(DateTime date) : base($"Reservation date {date.ToShortDateString()} is invalid")
    {
    }
}