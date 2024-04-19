using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.OrderItems.Dtos;

namespace WebShop.Application.OrderItems.Queries;

public class GetOrderItemByIdQuery : IRequest<OrderItemDto>
{
    public GetOrderItemByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; }
}
