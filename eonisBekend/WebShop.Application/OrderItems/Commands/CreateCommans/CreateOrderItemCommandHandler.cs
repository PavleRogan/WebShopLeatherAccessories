using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;

namespace WebShop.Application.OrderItems.Commands.CreateCommans;

public class CreateOrderItemCommandHandler(
    ILogger<CreateOrderItemCommandHandler> logger,
    IMapper mapper, IOrderItemRepository orderItemRepository) : IRequestHandler<CreateOrderItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new orderItem: {@orderItem}", request);
        var oi = mapper.Map<OrderItem>(request);
        Guid id = await orderItemRepository.Create(oi);
        return id;
    }
}
