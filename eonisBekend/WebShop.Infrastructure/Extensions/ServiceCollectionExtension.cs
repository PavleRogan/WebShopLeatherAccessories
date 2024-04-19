using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Persistence;
using WebShop.Infrastructure.Repositories;
using WebShop.Infrastructure.Seeders;

namespace WebShop.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WebShopDb");
        services.AddDbContext<WebShopDbContext>(options => 
        options
        .UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());

        services.AddScoped<IDataSeeder, DataSeeder>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
    }

} 
