using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Products.Queries.GetById;

public class GetProductByIdQuery : IRequest<ProductDto>
{
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; }
}
