using MySpot.Core.Exceptions;

class InvalidFullNameException(string value) : CustomException($"Invalid full name {value}");