using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Commands.CreateCommands;

public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger,
    IMapper mapper, IUsersRepository usersRepository) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        string normalizedEmail = request.Email.ToLowerInvariant();
        logger.LogInformation("Creating new user: {@user}", request);

        var existingUser = await usersRepository.GetByEmail(normalizedEmail);
        if (existingUser == null)
        {
            var user = mapper.Map<User>(request);
            user.Email = normalizedEmail;
            Guid id = await usersRepository.Create(user);
            return id;
        }
        else
        {
            throw new NotFoundException(nameof(User), request.Email.ToString());
        }
       
    }
}
