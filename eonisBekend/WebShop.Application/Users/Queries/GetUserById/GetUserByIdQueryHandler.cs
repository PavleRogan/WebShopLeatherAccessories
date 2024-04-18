

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger,
    IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting user by id:{id}", request.Id);
        var user = await usersRepository.GetById(request.Id);
        var userDto = mapper.Map<UserDto>(user);
        return userDto;
    }
}
