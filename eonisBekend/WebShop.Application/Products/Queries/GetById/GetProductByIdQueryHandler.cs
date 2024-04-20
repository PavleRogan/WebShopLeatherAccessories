using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Products.Queries.GetById;

public class GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger,
    IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product by id:{id}", request.Id);
        var product = await productsRepository.GetById(request.Id)
            ?? throw new NotFoundException(nameof(Product), request.Id.ToString());
        var productDto = mapper.Map<ProductDto>(product);
        return productDto;
    }
}
