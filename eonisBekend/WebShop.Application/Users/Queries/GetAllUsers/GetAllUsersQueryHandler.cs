
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(ILogger<GetAllUsersQueryHandler> logger,
    IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {

        logger.LogInformation("Getting all restaurants");
        var users = await usersRepository.GetAllAsync();
        var usersDtos = mapper.Map<IEnumerable<UserDto>>(users);
        return usersDtos;
    }
}
