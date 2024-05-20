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

        public async Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? category = null, string? gender = null)
        {
            var searchPhraseLow = searchPhrase?.ToLower();
            var categoryLow = category?.ToLower();
            var genderLow = gender?.ToLower();

            var baseQuery = dbContext.Products.Where(
                p => (searchPhrase == null || p.Name.ToLower().Contains(searchPhraseLow)) &&
                     (category == null || p.Category.ToLower() == categoryLow) &&
                     (gender == null || p.Gender.ToLower() == genderLow)
            );

            var totalCount = await baseQuery.CountAsync();

            var products = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }


        public async Task<Product?> GetById(Guid productId)
        {
            var product = await dbContext.Products 
            .FirstOrDefaultAsync(x => x.ProductId == productId);
            return product;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();

        public async Task<IEnumerable<Product>> GetByGender(string gender)
        {
            var products = await dbContext.Products
            .Where(u => u.Gender == gender)
            .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByCategory(string category)
        {
            var products = await dbContext.Products
                .Where(u => u.Category == category).ToListAsync();
            return products;
        }

       
    }
}
