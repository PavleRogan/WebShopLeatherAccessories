using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Admins.Dtos;

namespace WebShop.Application.Admins.Queries.GetAll;

public class GetAllAdminsQuery : IRequest<IEnumerable<AdminDto>>
{
}
