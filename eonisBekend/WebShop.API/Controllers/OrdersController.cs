using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Orders.Commands;
using WebShop.Application.Orders.Commands.DeleteCommands;
using WebShop.Application.Orders.Commands.UpdateCommand;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Orders.Queries;
using WebShop.Application.Users.Commands.DeleteCommands;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Application.Users.Dtos;
using WebShop.Application.Users.Queries.GetAllUsers;
using WebShop.Application.Users.Queries.GetUserById;
using WebShop.Domain.Entities;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/orders")]
//[Authorize]
public class OrdersController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var orders = await mediator.Send(new GetAllOrdersQuery());
        if (orders == null || !orders.Any())
        {
            return NoContent();
        }
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
         Guid orderId =  await mediator.Send(command);
        return CreatedAtAction(nameof(GetOrderById), new { orderId }, null);

    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var user = await mediator.Send(new GetOrderByIdQuery(orderId));

        return Ok(user);
    }

    [HttpPatch("{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(Guid orderId, UpdateOrderCommand command)
    {
        command.OrderId = orderId;
        await mediator.Send(command);
        return NoContent();


    }


    [HttpDelete("{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(Guid orderId)
    {
        await mediator.Send(new DeleteOrderCommand(orderId));

        return NoContent();
    }

}
