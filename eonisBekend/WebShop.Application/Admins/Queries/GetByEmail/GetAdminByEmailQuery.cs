using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Admins.Dtos;
using WebShop.Application.Users.Dtos;

namespace WebShop.Application.Admins.Queries.GetByEmail;

public class GetAdminByEmailQuery : IRequest<AdminDto>
{
    public GetAdminByEmailQuery(string email)
    {
        Email = email;
    }
    public string Email { get; }
}
