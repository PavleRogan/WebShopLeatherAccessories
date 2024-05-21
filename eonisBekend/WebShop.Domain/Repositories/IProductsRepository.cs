using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Constants;
using WebShop.Domain.Entities;

namespace WebShop.Domain.Repositories;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetById(Guid productId);

    Task<IEnumerable<Product>> GetByGender(string gender);

    Task<IEnumerable<Product>> GetByCategory(string category);
    Task<Guid> Create(Product entity);
    
    Task Delete(Product entity);

    Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(int pageSize, int pageNumber, string? searchPhrase, string? category, string? gender, string? sortBy, SortDirection sortDirection);


    Task SaveChanges();
}
