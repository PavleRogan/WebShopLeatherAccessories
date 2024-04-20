using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Repositories;

namespace WebShop.API.Helpers;

public class AuthHelper(IUsersRepository usersRepository, 
    IAdminsRepository adminsRepository) : IAuthHelper
{
    public bool AuthenticateUser(AuthCreds authCreds)
    {
        if (usersRepository.UserWithCredentialsExists(authCreds.Email, authCreds.Password))
        {
            return true;
        }

        if (adminsRepository.AdminWithCredentialsExists(authCreds.Email, authCreds.Password))
        {
            return true;
        }

        return false;

    }

    public string GenerateJwt(AuthCreds authCreds)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123QFpwJWXcOW6yjJyz666WU+yxnXbRnbMWzQVB/Vqc="));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        string role = IsAdmin(authCreds) ? "Admin" : "User";

        var claims = new[]
        {
                new Claim(ClaimTypes.Email, authCreds.Email),
                new Claim(ClaimTypes.Role, role)
            };

        var token = new JwtSecurityToken("EONIS.uns.ac.rs",
                                         "EONIS.uns.ac.rs",
                                         claims,
                                         expires: DateTime.Now.AddMinutes(120),
                                         signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool IsAdmin(AuthCreds authCreds)
    {
        if (adminsRepository.AdminWithCredentialsExists(authCreds.Email, authCreds.Password))
        {
            return true;
        }

        return false;
    }
}
