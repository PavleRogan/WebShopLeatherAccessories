using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Admins.Commands.CreateCommands;
using WebShop.Application.Admins.Commands.DeleteCommands;
using WebShop.Application.Admins.Commands.UpdateCommands;
using WebShop.Application.Admins.Dtos;
using WebShop.Application.Admins.Queries.GetAll;
using WebShop.Application.Admins.Queries.GetById;
using WebShop.Domain.Entities;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/admins")]
[Authorize(Roles = "Admin")]
public class AdminsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AdminDto>>> GetAll()
    {
        var admins = await mediator.Send(new GetAllAdminsQuery());
        if (admins == null || !admins.Any())
        {
            return NoContent();
        }
        return Ok(admins);
    }

    [HttpGet("{adminId}")]
    public async Task<ActionResult<AdminDto>> GetById(Guid adminId)
    {
        var admin = await mediator.Send(new GetAdminByIdQuery(adminId));

        return Ok(admin);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAdminCommand command)
    {
        var adminId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById),new {adminId}, null);
    }

    [HttpDelete("adminId")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid adminId)
    {
        await mediator.Send(new DeleteAdminCommand(adminId));
        return NoContent();
    }

    [HttpPatch("{adminId}")]
    public async Task<IActionResult> Update([FromBody] UpdateAdminCommand command, Guid adminId)
    {
        command.AdminId = adminId;
        await mediator.Send(command);
        return NoContent();
    }

}
