
using MySpot.Core.Exceptions;

public class InvalidRoleException(string value) : CustomException($"Invalid role {value}")
{
}
