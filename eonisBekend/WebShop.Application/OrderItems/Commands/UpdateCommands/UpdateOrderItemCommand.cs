using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.OrderItems.Commands.UpdateCommands;

public class UpdateOrderItemCommand : IRequest
{
    public Guid OrderItemId { get; set; }
    public int Quantity { get; set; }  
}
