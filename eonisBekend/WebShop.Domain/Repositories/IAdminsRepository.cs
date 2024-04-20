using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.Domain.Repositories;

public interface IAdminsRepository
{
    Task<IEnumerable<Admin>> GetAllAsync();
    Task<Admin?> GetById(Guid adminId);

    Task<Guid> Create(Admin entity);

    Task Delete(Admin entity);

    Task SaveChanges();

    bool AdminWithCredentialsExists(string email, string password);
}
