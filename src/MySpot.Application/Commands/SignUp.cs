
using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands;

public record SignUp(
    Guid UserId, string Email, string Password, string Username, string FullName, string Role) : ICommand;
