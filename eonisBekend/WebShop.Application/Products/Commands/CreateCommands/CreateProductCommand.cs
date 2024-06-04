using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Products.Commands.CreateCommands;

public class CreateProductCommand  : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;

    public string Gender { get; set; } = default!;

    public string ImageUrl { get; set; } = default!;

    public decimal Price { get; set; }
    public int? StockQuantity { get; set; }
}
