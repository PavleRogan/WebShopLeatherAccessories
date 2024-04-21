using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Users;
using WebShop.Application.Users.Commands.CreateCommands;
using WebShop.Application.Users.Commands.DeleteCommands;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Application.Users.Dtos;
using WebShop.Application.Users.Queries.GetAllUsers;
using WebShop.Application.Users.Queries.GetByEmail;
using WebShop.Application.Users.Queries.GetUserById;
using WebShop.Domain.Entities;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Roles = "Admin")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var users = await mediator.Send(new GetAllUsersQuery());
        if (users == null || !users.Any())
        {
           return NoContent();
        }
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid userId)
    {
        var user = await mediator.Send(new GetUserByIdQuery(userId));
        
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string email)
    {
        var user = await mediator.Send(new GetUserByEmailQuery(email));

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
         await mediator.Send(new DeleteUserCommand(userId));
        
        return NoContent();
    }

    [HttpPatch("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserCommand command)
    {
        command.UserId = userId;
        await mediator.Send(command);
        return NoContent();
        

    }
}
