using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Users;
using WebShop.Application.Users.Commands.CreateCommands;
using WebShop.Application.Users.Commands.DeleteCommands;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Application.Users.Dtos;
using WebShop.Application.Users.Queries.GetAllUsers;
using WebShop.Application.Users.Queries.GetUserById;
using WebShop.Domain.Entities;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await mediator.Send(new GetAllUsersQuery());
        if (users == null || !users.Any())
        {
           return NoContent();
        }
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetById(Guid userId)
    {
        var user = await mediator.Send(new GetUserByIdQuery(userId));
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser( CreateUserCommand command)
    {
       
        Guid userId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { userId }, null);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var isDeleted = await mediator.Send(new DeleteUserCommand(userId));
        if (isDeleted)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPatch("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserCommand command)
    {
        command.UserId = userId;
        var isUpdated = await mediator.Send(command);
        if (isUpdated)
        {
            return NoContent();
        }
        return NotFound();

    }
}
