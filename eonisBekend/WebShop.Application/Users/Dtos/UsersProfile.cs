

using AutoMapper;
using WebShop.Domain.Entities;

namespace WebShop.Application.Users.Dtos;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserDto>()
        .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
        .ForMember(d => d.StreetAndNumber, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.StreetAndNumber))
        .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
        .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

        CreateMap<CreateUserDto, User>()
            .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    StreetAndNumber = src.StreetAndNumber
                }));
    }
}
  