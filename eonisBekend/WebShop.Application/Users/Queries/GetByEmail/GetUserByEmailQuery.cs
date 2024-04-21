using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Users.Dtos;

namespace WebShop.Application.Users.Queries.GetByEmail;

public class GetUserByEmailQuery : IRequest<UserDto>
{
    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
    public string Email { get; }
}