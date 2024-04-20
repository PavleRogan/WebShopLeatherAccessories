using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Admins.Commands.CreateCommands;

public class CreateAdminCommandHandler(ILogger<CreateAdminCommandHandler> logger,
    IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<CreateAdminCommand, Guid>
{
    public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new admin: {@admin}", request);
        var admin = mapper.Map<Admin>(request);
        Guid id = await adminsRepository.Create(admin);
        return id;
    }
}
