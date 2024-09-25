namespace MySpot.Core.Exceptions;
public class InvalidPasswordException(string value) : CustomException($"Invalid password {value}")
{

}