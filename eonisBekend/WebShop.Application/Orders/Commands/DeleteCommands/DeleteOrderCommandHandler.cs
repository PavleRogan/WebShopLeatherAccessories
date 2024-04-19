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

namespace WebShop.Application.Orders.Commands.DeleteCommands;

public class DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger,
    IMapper mapper, IOrdersRepository ordersRepository) : IRequestHandler<DeleteOrderCommand>
{
    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Order with id: {orderId}", request.Id);
        var order = await ordersRepository.GetById(request.Id);
        if (order is null)
        {
            throw new NotFoundException(nameof(Order), request.Id.ToString());
        }
        await ordersRepository.Delete(order);
    }
}
