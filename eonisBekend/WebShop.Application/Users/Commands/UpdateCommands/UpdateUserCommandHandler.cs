
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Commands.UpdateCommands;

public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
    IMapper mapper, 
    IUsersRepository usersRepository) : IRequestHandler<UpdateUserCommand, bool>
{
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating user with id {userId} with {@user}", request.UserId, request);

        var user = await usersRepository.GetById(request.UserId);
        if (user is null)
        {
            return false;
        }
        mapper.Map(request, user);
      
        await usersRepository.SaveChanges();
        return true;
        
    }
}
