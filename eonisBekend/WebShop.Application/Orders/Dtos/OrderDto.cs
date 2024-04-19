using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.OrderItems.Dtos;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Entities;

namespace WebShop.Application.Orders.Dtos;

public class OrderDto
{
    public Guid OrderId { get; set; }
     
    public DateTime OrderDate { get; set; }

    public bool Processed { get; set; }


    public List<OrderItemDto?>? OrderItems { get; set; }
}
