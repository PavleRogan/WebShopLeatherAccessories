using AutoMapper;
using WebShop.Application.OrderItems.Dtos;
using WebShop.Application.Orders.Commands;
using WebShop.Application.Orders.Commands.UpdateCommand;
using WebShop.Domain.Entities;

namespace WebShop.Application.Orders.Dtos;

public class OrdersProfile : Profile
{
    public OrdersProfile()
    {
        //CreateMap<Order, OrderDto>()
        //  .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(oi => new OrderItemDto
            {
                OrderItemId = oi.OrderItemId,
                ProductId = oi.ProductId,
                OrderId = oi.OrderId,
                Quantity = oi.Quantity
            }).ToList()));

        CreateMap<CreateOrderCommand, Order>();
        CreateMap<UpdateOrderCommand, Order>();

    }
}
