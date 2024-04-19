using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Users.Dtos;
using WebShop.Application.Users.Queries.GetAllUsers;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Orders.Queries;

public class GetAllOrdersQueryHandler(ILogger<GetAllOrdersQueryHandler> logger,
    IOrdersRepository ordersRepository,
    IMapper mapper) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all orders");
        var orders = await ordersRepository.GetAllAsync();
        var ordersDtos = mapper.Map<IEnumerable<OrderDto>>(orders);
        return ordersDtos;
    }
}

