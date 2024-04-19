
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Commands.UpdateCommands;

public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
    IMapper mapper, 
    IUsersRepository usersRepository) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating user with id {userId} with {@user}", request.UserId, request);

        var user = await usersRepository.GetById(request.UserId);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId.ToString());

        }
        mapper.Map(request, user);
      
        await usersRepository.SaveChanges();
        
    }
}
