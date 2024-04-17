using Microsoft.AspNetCore.Mvc;
using WebShop.Application.Users;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Entities;

namespace WebShop.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await usersService.GetAllUsers();
        if (users == null || !users.Any())
        {
            NoContent();
        }
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var user = await usersService.GetUserById(userId);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser( CreateUserDto createUserDto)
    {
       
        Guid userId = await usersService.Create(createUserDto);
        return CreatedAtAction(nameof(GetById), new { userId }, null);
    }
}
