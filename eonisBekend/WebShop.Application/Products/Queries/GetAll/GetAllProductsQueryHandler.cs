using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Common;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Products.Queries.GetAll;

public class GetAllProductsQueryHandler(ILogger<GetAllProductsQueryHandler> logger,
    IProductsRepository productsRepository,
    IMapper mapper) : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");
        var (products,totalCount) = await productsRepository.GetAllMatchingAsync(request.SearchPhrase, request.PageSize,
            request.PageNumber, request.Category, request.Gender);

        var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
        var result = new PagedResult<ProductDto>(productsDtos,totalCount, request.PageSize, request.PageNumber);
        return result;
    }

}
