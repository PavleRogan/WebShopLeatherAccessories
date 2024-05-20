using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common;

namespace WebShop.Application.Products.Queries.GetAll;

public class GetAllProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public string? SearchPhrase { get; set; }

    public string? Category { get; set; }
    public string? Gender { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
