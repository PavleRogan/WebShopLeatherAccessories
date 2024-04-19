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

internal class OrderItemRepository(WebShopDbContext dbContext) : IOrderItemRepository
{
    public async Task<Guid> Create(OrderItem entity)
    {
        dbContext.OrderItems.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.OrderItemId;
    }

    public async Task Delete(OrderItem entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        var orderItems = await dbContext.OrderItems.ToListAsync();
        return orderItems;
    }

    public async Task<OrderItem?> GetById(Guid orderItemId)
    {
        var order = await dbContext.OrderItems
           .FirstOrDefaultAsync(x => x.OrderItemId == orderItemId);
        return order;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

    
}
