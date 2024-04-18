
using MediatR;

namespace WebShop.Application.Users.Commands.UpdateCommands;

public class UpdateUserCommand : IRequest<bool>
{
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? ContactNumber { get; set; }
}
