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
    IOrdersRepository ordersRepository) : IRequestHandler<CreateOrderCommand>
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
            OrderDate = request.OrderDate,
            UserId = request.UserId,
            Processed = request.Processed,
            User = await usersRepository.GetById(request.UserId)
            
        };

        await ordersRepository.Create(order);

    }
}
