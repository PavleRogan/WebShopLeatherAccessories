
using MediatR;
using WebShop.Application.Users.Dtos;

namespace WebShop.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
