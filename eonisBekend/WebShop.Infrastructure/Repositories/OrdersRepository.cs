using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Persistence;

namespace WebShop.Infrastructure.Repositories;

internal class OrdersRepository(WebShopDbContext dbContext) : IOrdersRepository
{
    public async Task<Guid> Create(Order entity)
    {
        dbContext.Orders.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.OrderId;
    }

    public async Task Delete(Order entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orders = await dbContext.Orders
            .Include(x => x.OrderItems).ToListAsync();
        return orders;
    }

    public async Task<Order?> GetById(Guid orderId)
    {
        var order = await dbContext.Orders
           .Include(x => x.OrderItems)
           .FirstOrDefaultAsync(x => x.OrderId == orderId);
        return order;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
