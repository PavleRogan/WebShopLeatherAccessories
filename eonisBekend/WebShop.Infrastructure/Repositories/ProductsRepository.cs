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
    internal class ProductsRepository(WebShopDbContext dbContext) : IProductsRepository
    {
        public async Task<Guid> Create(Product entity)
        {
            dbContext.Products.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.ProductId;
        }

        public async Task Delete(Product entity)
        {

            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await dbContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetById(Guid productId)
        {
            var product = await dbContext.Products 
            .FirstOrDefaultAsync(x => x.ProductId == productId);
            return product;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
