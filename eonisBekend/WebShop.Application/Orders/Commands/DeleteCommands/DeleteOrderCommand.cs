using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Orders.Commands.DeleteCommands;

public class DeleteOrderCommand(Guid Id) : IRequest
{
    public Guid Id { get; } = Id;
}
