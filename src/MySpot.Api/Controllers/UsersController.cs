using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Security;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController(
    ICommandHandler<SignUp> signUpHandler,
    ICommandHandler<SignIn> signInHandler,
    IQueryHandler<GetUser, UserDto> getUserHandler,
    IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
    ITokenStorage tokenStorage
    ) : ControllerBase
{

    private readonly ICommandHandler<SignUp> _signUpHandler = signUpHandler;
    private readonly ICommandHandler<SignIn> _signInHandler = signInHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler = getUserHandler;
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler = getUsersHandler;
    private readonly ITokenStorage _tokenStorage = tokenStorage;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
    {
        return Ok(await _getUsersHandler.Handle(query));
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Get()
    {
        var userId = HttpContext.User.Identity?.Name;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return NotFound();
        }

        return Ok(await _getUserHandler.Handle(new GetUser(Guid.Parse(userId))));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        return Ok(await _getUserHandler.Handle(new GetUser(id)));
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _signUpHandler.Handle(command);
        return Created();
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<JwtDto>> Post(SignIn command)
    {
        await _signInHandler.Handle(command);
        return Ok(_tokenStorage.GetJwtDto());
    }

}