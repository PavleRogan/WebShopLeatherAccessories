using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.OrderItems.Dtos;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Orders.Queries;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.OrderItems.Queries;

public class GetOrderItemByIdQueryHandler(
    ILogger<GetAllOrderItemsQueryHandler> logger,
    IOrderItemRepository orderItemsRepository,
    IMapper mapper) : IRequestHandler<GetOrderItemByIdQuery, OrderItemDto>
{
    public async Task<OrderItemDto> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting orderItem by id:{id}", request.Id);
        var orderItem = await orderItemsRepository.GetById(request.Id)
            ?? throw new NotFoundException(nameof(OrderItem), request.Id.ToString());
        var orderItemDto = mapper.Map<OrderItemDto>(orderItem);
        return orderItemDto;
    }
}
