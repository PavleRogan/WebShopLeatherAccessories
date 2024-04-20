using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.OrderItems.Commands.CreateCommans;

public class CreateOrderItemCommandHandler(
    ILogger<CreateOrderItemCommandHandler> logger,
    IMapper mapper, IOrderItemRepository orderItemRepository,
   IOrdersRepository ordersRepository, 
   IProductsRepository productsRepository) : IRequestHandler<CreateOrderItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new orderItem: {@orderItem}", request);

        var order = await ordersRepository.GetById(request.OrderId);
        if (order is null)
        {
            throw new NotFoundException(nameof(Order), request.OrderId.ToString());
        }

        //isto za product

        //var oi = mapper.Map<OrderItem>(request);
        var oi = new OrderItem
        {
            OrderId = request.OrderId,
            Order = await ordersRepository.GetById(request.OrderId),
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Product = await productsRepository.GetById(request.ProductId)

        };
        Guid id = await orderItemRepository.Create(oi);
        return id;
    }
}
