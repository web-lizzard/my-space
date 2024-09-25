using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public class UserAlreadyInUseException(string value) : CustomException($"Already in use {value}") { }