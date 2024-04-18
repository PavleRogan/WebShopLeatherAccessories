using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Commands.CreateCommands;

public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger,
    IMapper mapper, IUsersRepository usersRepository) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new user: {@user}", request);
        var user = mapper.Map<User>(request);
        Guid id = await usersRepository.Create(user);
        return id;
    }
}
