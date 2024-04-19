using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.OrderItems.Commands.UpdateCommands;

public class UpdateOrderItemCommandHandler(ILogger<UpdateOrderItemCommandHandler> logger,
    IMapper mapper,
    IOrderItemRepository orderItemRepository) : IRequestHandler<UpdateOrderItemCommand>
{
    public async Task Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating orderItem with id {orderItemId} with {@orderItem}", request.OrderItemId, request);

        var oi = await orderItemRepository.GetById(request.OrderItemId);
        if (oi is null)
        {
            throw new NotFoundException(nameof(OrderItem), request.OrderItemId.ToString());

        }
        mapper.Map(request, oi);

        await orderItemRepository.SaveChanges();
    }
}
