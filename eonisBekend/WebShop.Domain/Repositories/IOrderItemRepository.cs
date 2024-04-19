using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.Domain.Repositories;

public interface IOrderItemRepository
{
    Task<Guid> Create(OrderItem entity);
    Task<OrderItem?> GetById(Guid orderItemId);
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task SaveChanges();

    Task Delete(OrderItem entity);
}
