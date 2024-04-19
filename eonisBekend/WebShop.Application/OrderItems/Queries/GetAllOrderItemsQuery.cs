using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.OrderItems.Dtos;

namespace WebShop.Application.OrderItems.Queries;

public class GetAllOrderItemsQuery : IRequest<IEnumerable<OrderItemDto>>
{
}
