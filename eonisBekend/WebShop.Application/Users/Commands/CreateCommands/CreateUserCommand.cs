

using MediatR;

namespace WebShop.Application.Users.Commands.CreateCommands;

public class CreateUserCommand : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? City { get; set; }
    public string? StreetAndNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? ContactNumber { get; set; }
}
