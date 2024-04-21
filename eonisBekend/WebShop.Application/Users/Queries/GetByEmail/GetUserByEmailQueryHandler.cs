using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Users.Dtos;
using WebShop.Application.Users.Queries.GetUserById;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Queries.GetByEmail;

public class GetUserByEmailQueryHandler(ILogger<GetUserByEmailQueryHandler> logger,
    IUsersRepository usersRepository,
    IMapper mapper) : IRequestHandler<GetUserByEmailQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting user by email:{email}", request.Email);
        var user = await usersRepository.GetByEmail(request.Email)
            ?? throw new NotFoundException(nameof(User), request.Email.ToString());
        var userDto = mapper.Map<UserDto>(user);
        return userDto;
    }
}