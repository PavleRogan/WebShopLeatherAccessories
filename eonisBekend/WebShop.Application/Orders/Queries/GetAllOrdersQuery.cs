using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Orders.Dtos;

namespace WebShop.Application.Orders.Queries;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}
