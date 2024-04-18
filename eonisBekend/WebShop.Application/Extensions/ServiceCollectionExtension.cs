

using Microsoft.Extensions.DependencyInjection;
using System.Data;
using WebShop.Application.Users;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var appAssembly = typeof(ServiceCollectionExtension).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(appAssembly));
        services.AddAutoMapper(appAssembly);
    }
}
