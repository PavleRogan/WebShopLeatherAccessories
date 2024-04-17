using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Persistence;

namespace WebShop.Infrastructure.Repositories;

internal class UsersRepository(WebShopDbContext dbContext) : IUsersRepository
{
    public async Task<Guid> Create(User entity)
    {
        dbContext.Users.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.UserId;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await dbContext.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetById(Guid userId)
    {
        var user = await dbContext.Users
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.UserId == userId);
        return user;
    }
}
