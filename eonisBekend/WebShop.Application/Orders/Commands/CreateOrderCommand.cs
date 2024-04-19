using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.OrderItems.Dtos;
using WebShop.Domain.Entities;

namespace WebShop.Application.Orders.Commands;

public class CreateOrderCommand : IRequest
{

    public DateTime OrderDate { get; set; }

    public Guid UserId { get; set; }

    public bool Processed { get; set; }


}
