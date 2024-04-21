using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Products.Queries.GetByGender;

public class GetProductsByGenderQueryHandler(ILogger<GetProductsByGenderQueryHandler> logger,
    IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<GetProductsByGenderQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsByGenderQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product by gender:{gender}", request.Gender);
        var products = await productsRepository.GetByGender(request.Gender)
            ?? throw new NotFoundException(nameof(Product), request.Gender.ToString());

        var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
        return productsDtos;
    }
}
