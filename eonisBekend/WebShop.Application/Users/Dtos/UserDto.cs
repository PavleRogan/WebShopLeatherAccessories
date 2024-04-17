

using WebShop.Application.Orders;
using WebShop.Application.Orders.Dtos;
using WebShop.Domain.Entities;

namespace WebShop.Application.Users.Dtos;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public string? City { get; set; }
    public string? StreetAndNumber { get; set; }
    public string? PostalCode { get; set; }

    //public string? ContactNumber { get; set; }

    public List<OrderDto>? Orders { get; set; }

}
