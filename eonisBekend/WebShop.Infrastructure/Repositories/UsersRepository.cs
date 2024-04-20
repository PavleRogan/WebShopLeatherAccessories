using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Persistence;

namespace WebShop.Infrastructure.Repositories;

internal class UsersRepository(WebShopDbContext dbContext) : IUsersRepository
{
    private readonly static int iterations = 1000;
    public async Task<Guid> Create(User entity)
    {
        dbContext.Users.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.UserId;
    }

    public async Task Delete(User entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
        
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await dbContext.Users.Include(x => x.Orders).ToListAsync();
        return users;
    }

    public async Task<User?> GetById(Guid userId)
    {
        var user = await dbContext.Users
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.UserId == userId);
        return user;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

    public bool UserWithCredentialsExists(string email, string password)
    {
        User user = dbContext.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            return false;
        }

        if (VerifyPassword(password, user.Password, user.Salt))
        {
            return true;
        }
        return false;
    }

    private Tuple<string, string> HashPassword(string password)
    {
        var sBytes = new byte[password.Length];
        new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
        var salt = Convert.ToBase64String(sBytes);

        var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

        return new Tuple<string, string>
        (
            Convert.ToBase64String(derivedBytes.GetBytes(256)),
            salt
        );
    }

    public bool VerifyPassword(string password, string savedHash, string savedSalt)
    {
        var saltBytes = Convert.FromBase64String(savedSalt);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
        if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
        {
            return true;
        }
        return false;
    }
}
