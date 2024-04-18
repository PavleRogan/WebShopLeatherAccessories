
using MediatR;

namespace WebShop.Application.Users.Commands.DeleteCommands;

public class DeleteUserCommand(Guid Id) : IRequest<bool>
{ 
    public Guid Id { get;} = Id;
}
