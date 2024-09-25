using MySpot.Core.Exceptions;

public class InvalidEmailException(string value) : CustomException($"Invalid email {value}")
{
}
