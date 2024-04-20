using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.OrderItems.Dtos;
using WebShop.Domain.Entities;

namespace WebShop.Application.Products
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;

        public string Gender { get; set; } = default!;

        public decimal Price { get; set; }
        public int? StockQuantity { get; set; }
    }
}
