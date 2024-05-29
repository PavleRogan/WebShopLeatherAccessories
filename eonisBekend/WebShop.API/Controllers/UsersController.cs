using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Orders.Queries.GetByUserId;
using WebShop.Application.Users;
using WebShop.Application.Users.Commands.CreateCommands;
using WebShop.Application.Users.Commands.DeleteCommands;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Application.Users.Dtos;
using WebShop.Application.Users.Queries.GetAllUsers;
using WebShop.Application.Users.Queries.GetByEmail;
using WebShop.Application.Users.Queries.GetUserById;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "Admin")]

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
    [Authorize]

    public async Task<ActionResult<OrderDto>> GetById(Guid userId)
    {
        var user = await mediator.Send(new GetUserByIdQuery(userId));
        
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    [Authorize]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
         await mediator.Send(new DeleteUserCommand(userId));
        
        return NoContent();
    }

    [HttpPatch("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserCommand command)
    {
        command.UserId = userId;
        await mediator.Send(command);
        return NoContent();
       
    }

    [HttpGet("{userId}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders(Guid userId)
    {
        var orders = await mediator.Send(new GetOrdersByUserId(userId));
        if (orders == null || !orders.Any())
        {
            return NoContent();
        }
        return Ok(orders);
    }

}
