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

namespace WebShop.Application.Orders.Commands.UpdateCommand;

public class UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger,
    IMapper mapper,
    IOrdersRepository ordersRepository) : IRequestHandler<UpdateOrderCommand>
{
    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating order with id {orderId} with {@order}", request.OrderId, request);

        var order = await ordersRepository.GetById(request.OrderId);
        if (order is null)
        {
            throw new NotFoundException(nameof(User), request.OrderId.ToString());

        }
        mapper.Map(request, order);

        await ordersRepository.SaveChanges();
    }
}
