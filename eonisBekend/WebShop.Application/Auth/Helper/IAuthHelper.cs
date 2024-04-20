
using WebShop.Domain.Entities;

namespace WebShop.API.Helpers;

public interface IAuthHelper
{
    public bool AuthenticateUser(AuthCreds authCreds);

    public bool IsAdmin(AuthCreds authCreds);

    public string GenerateJwt(AuthCreds authCreds);
}
