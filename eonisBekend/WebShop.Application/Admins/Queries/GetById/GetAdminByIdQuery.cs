using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Admins.Dtos;

namespace WebShop.Application.Admins.Queries.GetById;

public class GetAdminByIdQuery: IRequest<AdminDto>
{
    public GetAdminByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; }
}
