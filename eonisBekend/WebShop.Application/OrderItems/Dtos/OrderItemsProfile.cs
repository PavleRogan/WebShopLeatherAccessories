using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.OrderItems.Commands.CreateCommans;
using WebShop.Application.OrderItems.Commands.UpdateCommands;
using WebShop.Domain.Entities;

namespace WebShop.Application.OrderItems.Dtos
{
    public class OrderItemsProfile : Profile
    {
        public OrderItemsProfile()
        {
             CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<UpdateOrderItemCommand, OrderItem>();
              CreateMap<CreateOrderItemCommand, OrderItem>();
           
        }

    }
    
}
