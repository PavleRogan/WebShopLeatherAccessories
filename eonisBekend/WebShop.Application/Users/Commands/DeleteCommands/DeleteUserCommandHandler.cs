
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Commands.DeleteCommands;

public class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger,
    IMapper mapper,
    IUsersRepository usersRepository) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting User with id: {userId}", request.Id);
        var user = await usersRepository.GetById(request.Id);
        if(user is null)
        {
            return false;
        }
        await usersRepository.Delete(user);
        return true;
        
    }
}
