using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Admins.Commands.UpdateCommands;

public class UpdateAdminCommandHandler(ILogger<UpdateAdminCommandHandler> logger,
    IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<UpdateAdminCommand>
{
    public async Task Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating admin with id {adminId} with {@admin}", request.AdminId, request);

        var admin = await adminsRepository.GetById(request.AdminId);
        if (admin is null)
        {
            throw new NotFoundException(nameof(Admin), request.AdminId.ToString());

        }
        mapper.Map(request, admin);

        await adminsRepository.SaveChanges();
    }
}
