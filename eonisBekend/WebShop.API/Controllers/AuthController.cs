using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebShop.API.Helpers;
using WebShop.Application.Users.Commands.CreateCommands;
using WebShop.Domain.Entities;

namespace WebShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthHelper authHelper) : ControllerBase
    {

        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate(AuthCreds authCred)
        {
           
            if (authHelper.AuthenticateUser(authCred))
            {
                var tokenString = authHelper.GenerateJwt(authCred);
                return Ok(new { token = tokenString, authCred.Email });
            }
            return Unauthorized();
        }



    }
}
