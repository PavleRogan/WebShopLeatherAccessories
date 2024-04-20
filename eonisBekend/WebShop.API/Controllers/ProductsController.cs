using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Products;
using WebShop.Application.Products.Commands.CreateCommands;
using WebShop.Application.Products.Commands.DeleteCommands;
using WebShop.Application.Products.Commands.UpdateCommands;
using WebShop.Application.Products.Queries.GetAll;
using WebShop.Application.Products.Queries.GetById;
using WebShop.Application.Users.Commands.CreateCommands;
using WebShop.Application.Users.Commands.DeleteCommands;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Application.Users.Queries.GetAllUsers;
using WebShop.Application.Users.Queries.GetUserById;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var p = await mediator.Send(new GetAllProductsQuery());
        if (p == null || !p.Any())
        {
            return NoContent();
        }
        return Ok(p);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid productId)
    {
        var p = await mediator.Send(new GetProductByIdQuery(productId));

        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {

        Guid productId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { productId }, null);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid productId)
    {
        await mediator.Send(new DeleteProductCommand(productId));

        return NoContent();
    }

    [HttpPatch("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid productId, UpdateProductCommand command)
    {
        command.ProductId = productId;
        await mediator.Send(command);
        return NoContent();

    }
}
