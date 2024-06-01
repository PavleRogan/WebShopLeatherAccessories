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
    IOrderItemRepository orderItemRepository) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new order {@OrderRequest}", request);

        var user = await usersRepository.GetById(request.UserId);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId.ToString());
        }

        // Check if there is enough stock for all products
        foreach (var product in request.OrderItems)
        {
            var prod = await productsRepository.GetById(product.ProductId);
            if (prod is null || prod.StockQuantity < product.Quantity)
            {
                throw new NotFoundException(nameof(Product), product.ProductId.ToString());
            }
        }

        var order = new Order
        {
            OrderId = request.OrderID,
            OrderDate = DateTime.Now,
            UserId = request.UserId,
            Processed = false,
            User = user
        };

        foreach (var product in request.OrderItems)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderItemId = Guid.NewGuid();
            orderItem.ProductId = product.ProductId;
            orderItem.OrderId = order.OrderId;
            orderItem.Product = await productsRepository.GetById(product.ProductId);
            orderItem.Order = order;
            orderItem.Quantity = product.Quantity;
            orderItem.Price = product.Price;
            orderItem.Name = product.Name;

            await orderItemRepository.Create(orderItem);

            // Update product stock quantity
            var prod = await productsRepository.GetById(product.ProductId);
            prod.StockQuantity -= product.Quantity;
            await productsRepository.SaveChanges();
        }

        //await ordersRepository.Create(order);

        return order.OrderId;

    }
}