
using WebShop.Domain.Entities;

namespace WebShop.Domain.Repositories;

public interface IOrdersRepository
{
    Task<Guid> Create(Order entity);
    Task<Order?> GetById(Guid orderId);
    Task<IEnumerable<Order>> GetAllAsync();
    Task SaveChanges();

    Task Delete(Order entity);

}
