using AutoMapper;
using WebShop.Domain.Entities;

namespace WebShop.Application.Orders.Dtos;

public class OrdersProfile : Profile
{
    public OrdersProfile()
    {
        CreateMap<Order, OrderDto>();
             
    }
}
