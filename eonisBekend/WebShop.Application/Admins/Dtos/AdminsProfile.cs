using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Admins.Commands.CreateCommands;
using WebShop.Application.Admins.Commands.UpdateCommands;
using WebShop.Domain.Entities;

namespace WebShop.Application.Admins.Dtos;

public class AdminsProfile : Profile
{
    public AdminsProfile()
    {
           CreateMap<AdminDto, Admin>();
        CreateMap<Admin, AdminDto>();
        CreateMap<CreateAdminCommand, Admin>();
        CreateMap<UpdateAdminCommand, Admin>();
    }
}
