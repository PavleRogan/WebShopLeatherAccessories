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

namespace WebShop.Application.Products.Commands.UpdateCommands;

public class UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger,
    IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with id {productId} with {@product}", request.ProductId, request);

        var p = await productsRepository.GetById(request.ProductId);
        if (p is null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        }
        mapper.Map(request, p);

        await productsRepository.SaveChanges();
    }
}
