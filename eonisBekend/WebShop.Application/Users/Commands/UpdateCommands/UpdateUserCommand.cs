
using MediatR;

namespace WebShop.Application.Users.Commands.UpdateCommands;

public class UpdateUserCommand : IRequest
{
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
        public string? ContactNumber { get; set; }

        public string? City { get; set; }

       public string? StreetAndNumber { get; set; }

        public string? PostalCode { get; set; }


}
