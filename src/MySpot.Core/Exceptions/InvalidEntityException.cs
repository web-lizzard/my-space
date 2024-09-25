namespace MySpot.Core.Exceptions;
public class InvalidEntityIdException(Guid value) : CustomException($"Invalid ID {value}");