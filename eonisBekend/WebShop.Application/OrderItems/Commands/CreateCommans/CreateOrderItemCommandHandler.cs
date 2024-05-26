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
        
   
        var oi = new OrderItem
        {
            OrderId = request.OrderId,
            Order = order,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Price = request.Price,
            Name = request.Name,
            Product = await productsRepository.GetById(request.ProductId)
    };
        

        if(oi.Quantity > oi.Product.StockQuantity)
        {
            throw new NotFoundException(nameof(Order), request.OrderId.ToString());
        }
        else
        {
            oi.Product.StockQuantity = oi.Product.StockQuantity - oi.Quantity;
            await productsRepository.SaveChanges();
            Guid id = await orderItemRepository.Create(oi);
            return id;
        }
        
    }
}
