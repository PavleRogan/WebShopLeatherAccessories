using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Orders.Dtos;
using WebShop.Domain.Entities;

namespace WebShop.Application.Orders.Queries.GetByUserId;

public class GetOrdersByUserId : IRequest<IEnumerable<OrderDto>>
{
    public GetOrdersByUserId(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}
