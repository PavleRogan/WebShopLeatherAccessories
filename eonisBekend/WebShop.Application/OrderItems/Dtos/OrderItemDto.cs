using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Products;
using WebShop.Domain.Entities;

namespace WebShop.Application.OrderItems.Dtos
{
    public class OrderItemDto
    {
        //public Guid OrderItemId { get; set; }

        public Guid ProductId { get; set; }

        //public Guid OrderId { get; set; }

        public int Quantity { get; set; }

    }
}
