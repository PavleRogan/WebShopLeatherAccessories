using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Products.Commands.CreateCommands;

public class CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger,
    IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating newproduct {@product}", request);
        var product = mapper.Map<Product>(request);
        Guid id = await productsRepository.Create(product);
        return id;
    }
}
