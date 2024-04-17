

using AutoMapper;
using Microsoft.Extensions.Logging;
using WebShop.Application.Users.Dtos;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users;

internal class UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger, IMapper mapper) : IUsersService
{
    public async Task<Guid> Create(CreateUserDto dto)
    {
        logger.LogInformation("Creating user");
        var user = mapper.Map<User>(dto);
        Guid id = await usersRepository.Create(user);
        return id;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        logger.LogInformation("Getting all restaurants");
        var users = await usersRepository.GetAllAsync();
        var usersDtos = mapper.Map<IEnumerable<UserDto>>(users);
        return usersDtos;
    }

    public async Task<UserDto?> GetUserById(Guid userId)
    {
        logger.LogInformation("Getting user by id:" + $"{userId}");
        var user = await usersRepository.GetById(userId);
        var userDto = mapper.Map<UserDto>(user);
        return userDto;
    }
}
