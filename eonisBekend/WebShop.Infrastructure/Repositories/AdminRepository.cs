using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Persistence;

namespace WebShop.Infrastructure.Repositories
{
    internal class AdminRepository(WebShopDbContext dbContext) : IAdminsRepository
    {
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
    }
}
