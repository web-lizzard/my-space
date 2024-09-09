namespace MySpot.Api.Exceptions;

public sealed class InvalidEmployeeName : CustomException
{
    public InvalidEmployeeName(string? name) : base($"Invalid Employee Name! {name}")
    {
    }
}


