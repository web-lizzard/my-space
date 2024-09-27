using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Application.Security;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Time;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;


internal sealed class SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock) : ICommandHandler<SignUp>

{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordManager _passwordManager = passwordManager;
    private readonly IClock _clock = clock;

    public async Task Handle(SignUp command)
    {
        var email = new Email(command.Email);
        var isUserAlreadyExist = await _userRepository.FindByEmail(command.Email);

        if (isUserAlreadyExist is not null)
        {
            throw new UserAlreadyInUseException(command.Email);
        }

        isUserAlreadyExist = await _userRepository.FindByName(command.FullName);

        if (isUserAlreadyExist is not null)
        {
            throw new UserAlreadyInUseException(command.FullName);
        }

        var securedPassword = _passwordManager.Secure(command.Password);
        var user = new User(command.UserId, email, securedPassword, command.Username, command.FullName, command.Role, command.JobTitle, _clock.Current());

        await _userRepository.Save(user);
    }
}
