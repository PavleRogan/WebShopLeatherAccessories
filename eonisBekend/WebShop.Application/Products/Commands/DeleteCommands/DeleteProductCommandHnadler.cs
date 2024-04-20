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

namespace WebShop.Application.Products.Commands.DeleteCommands;

public class DeleteProductCommandHnadler(ILogger<DeleteProductCommandHnadler> logger,
    IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete product with id: {productId}", request.Id);
        var p = await productsRepository.GetById(request.Id);
        if (p is null)
        {
            throw new NotFoundException(nameof(Product), request.Id.ToString());
        }
        await productsRepository.Delete(p);
    }
}
