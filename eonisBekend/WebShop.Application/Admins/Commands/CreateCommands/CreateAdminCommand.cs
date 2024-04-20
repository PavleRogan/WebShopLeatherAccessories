using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Admins.Commands.CreateCommands;

public class CreateAdminCommand :IRequest<Guid>
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
