using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController(
    ICommandHandler<SignUp> signUpHandler,
    IQueryHandler<GetUser, UserDto> getUserHandler,
    IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler) : ControllerBase
{

    private readonly ICommandHandler<SignUp> _signUpHandler = signUpHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler = getUserHandler;
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler = getUsersHandler;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        return Ok(await _getUsersHandler.Handle(new GetUsers()));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        return Ok(await _getUserHandler.Handle(new GetUser(id)));
    }

    [HttpPost]
    public async Task<ActionResult> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _signUpHandler.Handle(command);
        return Created();
    }
}