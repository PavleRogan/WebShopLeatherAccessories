using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Users.Commands.DeleteCommands;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.OrderItems.Commands.DeleteCommands;


public class DeleteOrderItemCommandHandler(ILogger<DeleteOrderItemCommandHandler> logger,
    IMapper mapper,
    IOrderItemRepository orderItemRepository) : IRequestHandler<DeleteOrderItemCommand>
{
    public async Task Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting OrderItem with id: {orderItemId}", request.Id);
        var oi = await orderItemRepository.GetById(request.Id);
        if (oi is null)
        {
            throw new NotFoundException(nameof(OrderItem), request.Id.ToString());
        }
        await orderItemRepository.Delete(oi);
    }
}
