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

namespace WebShop.Application.Admins.Commands.DeleteCommands;

public class DeleteAdminCommandHandler(ILogger<DeleteAdminCommandHandler> logger,
    IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<DeleteAdminCommand>
{
    public async Task Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Admin with id: {adminId}", request.Id);
        var admin = await adminsRepository.GetById(request.Id);
        if (admin is null)
        {
            throw new NotFoundException(nameof(Admin), request.Id.ToString());
        }
        await adminsRepository.Delete(admin);
    }
}
