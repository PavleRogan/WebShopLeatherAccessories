using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Products.Queries.GetAll;

public class GetAllProductsQueryHandler(ILogger<GetAllProductsQueryHandler> logger,
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {

        logger.LogInformation("Getting all products");
        var products = await productsRepository.GetAllAsync();
        var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
        return productsDtos;
    }
}
