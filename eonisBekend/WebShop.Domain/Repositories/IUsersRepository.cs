

using WebShop.Domain.Entities;

namespace WebShop.Domain.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetById(Guid userId);

    Task<Guid> Create(User entity);

    Task Delete(User entity);

    Task SaveChanges();


}
