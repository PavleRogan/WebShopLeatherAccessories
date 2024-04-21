using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Products.Queries.GetByGender;

public class GetProductsByGenderQuery : IRequest<IEnumerable<ProductDto>>
{
    public GetProductsByGenderQuery(string gender)
    {
        this.Gender = gender;
    }

    public string Gender { get; }
}
