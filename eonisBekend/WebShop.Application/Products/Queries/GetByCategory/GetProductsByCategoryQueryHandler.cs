using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Products.Queries.GetByGender;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Products.Queries.GetByCategory;

public class GetProductsByCategoryQueryHandler(ILogger<GetProductsByCategoryQueryHandler> logger,
    IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<GetProductsByCategoryQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product by category:{category}", request.Category);
        var products = await productsRepository.GetByCategory(request.Category)
            ?? throw new NotFoundException(nameof(Product), request.Category.ToString());

        var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
        return productsDtos;
    }
}
