
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.Exceptions;
using MySpot.Application.Security;
using MySpot.Core.Repositories;

internal sealed class SignInHandler(
    IUserRepository repository,
    IAuthenticator authenticator,
    IPasswordManager passwordManager,
    ITokenStorage tokenStorage
    ) : ICommandHandler<SignIn>
{
    private readonly IUserRepository _repository = repository;
    private readonly IAuthenticator _authenticator = authenticator;
    private readonly IPasswordManager _passwordManager = passwordManager;
    private readonly ITokenStorage _tokenStorage = tokenStorage;

    public async Task Handle(SignIn command)
    {
        var user = await _repository.FindByEmail(command.Email)
            ?? throw new InvalidCredentialsException();

        if (!_passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var token = _authenticator.CreateToken(user.Id, user.Role);
        _tokenStorage.Set(token);

    }
}