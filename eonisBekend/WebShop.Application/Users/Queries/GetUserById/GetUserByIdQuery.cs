
using MediatR;
using WebShop.Application.Users.Dtos;

namespace WebShop.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<UserDto?>
{
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; }
}

