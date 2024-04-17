

using Microsoft.Extensions.DependencyInjection;
using System.Data;
using WebShop.Application.Users;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);
    }
}
