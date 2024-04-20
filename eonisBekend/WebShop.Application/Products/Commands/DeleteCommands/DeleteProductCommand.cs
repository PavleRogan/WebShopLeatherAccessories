using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Products.Commands.DeleteCommands;

public class DeleteProductCommand(Guid Id) : IRequest
{
    public Guid Id { get; } = Id;
}
