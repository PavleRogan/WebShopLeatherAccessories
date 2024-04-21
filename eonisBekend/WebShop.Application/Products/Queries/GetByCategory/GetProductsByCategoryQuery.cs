using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Products.Queries.GetByCategory;

public class GetProductsByCategoryQuery : IRequest<IEnumerable<ProductDto>>
{
    public GetProductsByCategoryQuery(string category)
    {
        this.Category = category;
    }

    public string Category { get; }
}
