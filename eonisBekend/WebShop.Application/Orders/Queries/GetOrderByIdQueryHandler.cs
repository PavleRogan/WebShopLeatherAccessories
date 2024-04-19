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
using WebShop.Application.Users.Queries.GetUserById;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Orders.Queries;

public class GetOrderByIdQueryHandler(
    ILogger<GetOrderByIdQueryHandler> logger,
    IOrdersRepository ordersRepository,
    IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting order by id:{id}", request.Id);
        var order = await ordersRepository.GetById(request.Id)
            ?? throw new NotFoundException(nameof(Order), request.Id.ToString());
        var orderDto = mapper.Map<OrderDto>(order);
        return orderDto;
    }
}
