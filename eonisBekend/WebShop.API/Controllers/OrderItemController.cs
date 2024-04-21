using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.OrderItems.Commands.CreateCommans;
using WebShop.Application.OrderItems.Commands.DeleteCommands;
using WebShop.Application.OrderItems.Commands.UpdateCommands;
using WebShop.Application.OrderItems.Dtos;
using WebShop.Application.OrderItems.Queries;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Users.Commands.CreateCommands;
using WebShop.Application.Users.Commands.DeleteCommands;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Application.Users.Queries.GetAllUsers;
using WebShop.Application.Users.Queries.GetUserById;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/orderItems")]
[Authorize]
public class OrderItemController(IMediator mediator ) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll()
    {
        var orderItems = await mediator.Send(new GetAllOrderItemsQuery());
        if (orderItems == null || !orderItems.Any())
        {
            return NoContent();
        }
        return Ok(orderItems);
    }


    [HttpGet("{orderItemId}")]
    public async Task<ActionResult<OrderItemDto>> GetById(Guid orderItemId)
    {
        var orderItem = await mediator.Send(new GetOrderItemByIdQuery(orderItemId));

        return Ok(orderItem);
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrderItem(CreateOrderItemCommand command)
    {

        Guid orderItemId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { orderItemId }, null);
    }

    [HttpDelete("{orderItemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrderItem(Guid orderItemId)
    {
        await mediator.Send(new DeleteOrderItemCommand(orderItemId));

        return NoContent();
    }

    [HttpPatch("{orderItemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderItem(Guid orderItemId, UpdateOrderItemCommand command)
    {
        command.OrderItemId = orderItemId;
        await mediator.Send(command);
        return NoContent();


    }
}
