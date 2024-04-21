using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Products;
using WebShop.Application.Products.Commands.CreateCommands;
using WebShop.Application.Products.Commands.DeleteCommands;
using WebShop.Application.Products.Commands.UpdateCommands;
using WebShop.Application.Products.Queries.GetAll;
using WebShop.Application.Products.Queries.GetByCategory;
using WebShop.Application.Products.Queries.GetByGender;
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
    [Authorize]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {

        Guid productId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { productId }, null);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        await mediator.Send(new DeleteProductCommand(productId));

        return NoContent();
    }

    [HttpPatch("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid productId, UpdateProductCommand command)
    {
        command.ProductId = productId;
        await mediator.Send(command);
        return NoContent();

    }

    [HttpGet("gender/{gender}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByGender(string gender)
    {
        var products = await mediator.Send(new GetProductsByGenderQuery(gender));
        if (products == null || !products.Any())
        {
            return NotFound();
        }
        return Ok(products);
    }

    [HttpGet("category/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(string category)
    {
        var products = await mediator.Send(new GetProductsByCategoryQuery(category));
        if (products == null || !products.Any())
        {
            return NotFound();
        }
        return Ok(products);
    }
}

