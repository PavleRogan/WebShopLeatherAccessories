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

namespace WebShop.Application.Orders.Commands;

public class CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger,
    IMapper mapper, IUsersRepository usersRepository,
    IOrdersRepository ordersRepository,
    IProductsRepository productsRepository,
    IOrderItemRepository orderItemRepository) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new order {@OrderRequest}", request);

        var user = await usersRepository.GetById(request.UserId);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId.ToString());
        }

        var order = new Order
        { 
            OrderId = Guid.NewGuid(),
            OrderDate = DateTime.Now,
            UserId = request.UserId,
            Processed = false,
            User = await usersRepository.GetById(request.UserId)
            
        };

        foreach (var product in request.OrderItems) {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderItemId = Guid.NewGuid();
            orderItem.ProductId = product.ProductId;
            orderItem.OrderId = order.OrderId;
            orderItem.Product = await productsRepository.GetById(product.ProductId);
            orderItem.Order = order;
            orderItem.Quantity = product.Quantity;

            await orderItemRepository.Create(orderItem);

            var prod = await productsRepository.GetById(product.ProductId);
            if(prod is not null)
            {
                if (prod.StockQuantity >= product.Quantity)
                {

                    prod.StockQuantity = prod.StockQuantity - product.Quantity;
                }
                else
                {
                    throw new NotFoundException(nameof(Product), prod.ToString());
                }
            }
            await productsRepository.SaveChanges();

        }

           // await ordersRepository.Create(order);

    }
}
