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
using WebShop.Domain.Repositories;

namespace WebShop.Application.OrderItems.Queries;

public class GetAllOrderItemsQueryHandler(ILogger<GetAllOrderItemsQueryHandler> logger,
    IOrderItemRepository orderItemsRepository,
    IMapper mapper) : IRequestHandler<GetAllOrderItemsQuery, IEnumerable<OrderItemDto>>
{
    public async Task<IEnumerable<OrderItemDto>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all orderItems");
        var orderItems = await orderItemsRepository.GetAllAsync();
        var orderItemsDtos = mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        return orderItemsDtos;
    }
}
