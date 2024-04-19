
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Users.Commands.DeleteCommands;

public class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger,
    IMapper mapper,
    IUsersRepository usersRepository) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting User with id: {userId}", request.Id);
        var user = await usersRepository.GetById(request.Id);
        if(user is null)
        {
            throw new NotFoundException(nameof(User), request.Id.ToString());
        }
        await usersRepository.Delete(user);
        
    }
}
