using AutoMapper;
using WebShop.Application.Products.Commands.CreateCommands;
using WebShop.Application.Products.Commands.UpdateCommands;
using WebShop.Application.Users.Commands.CreateCommands;
using WebShop.Application.Users.Commands.UpdateCommands;
using WebShop.Domain.Entities;

namespace WebShop.Application.Products;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductCommand, Product>();
           

        CreateMap<UpdateProductCommand, Product>();

    }
}
