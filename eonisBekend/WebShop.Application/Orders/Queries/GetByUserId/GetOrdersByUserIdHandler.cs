using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Orders.Dtos;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Orders.Queries.GetByUserId;

public class GetOrdersByUserIdHandler(ILogger<GetOrderByIdQueryHandler> logger,
    IMapper mapper, IOrdersRepository ordersRepository) : IRequestHandler<GetOrdersByUserId, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByUserId request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting orders by userId: {userId}", request.UserId);
        var orders = await ordersRepository.GetByUserId(request.UserId)
            ?? throw new NotFoundException(nameof(Order), request.UserId.ToString());
        var orderDto = mapper.Map<IEnumerable<OrderDto>>(orders);
        return orderDto;
    }
}
