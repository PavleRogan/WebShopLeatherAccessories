using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Persistence;

namespace WebShop.Infrastructure.Repositories
{
    internal class AdminRepository(WebShopDbContext dbContext) : IAdminsRepository
    {
        private readonly static int iterations = 1000;

        public bool AdminWithCredentialsExists(string email, string password)
        {
            Admin admin = dbContext.Admins.FirstOrDefault(a => a.Username == email);

            if (admin == null)
            {
                return false;
            }

            if (VerifyPassword(password, admin.Password, admin.Salt))
            {
                return true;
            }
            return false;
        }

        public async Task<Guid> Create(Admin entity)
        {
            dbContext.Admins.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.AdminId;
        }

        public async Task Delete(Admin entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            var admins = await dbContext.Admins.ToListAsync();
            return admins;
        }

        public async Task<Admin?> GetById(Guid adminId)
        {
            var admin = await dbContext.Admins.FirstOrDefaultAsync(a => a.AdminId == adminId);
            return admin;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();

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


}
