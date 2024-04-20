using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Admins.Dtos;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Admins.Queries.GetById;

public class GetAdminByIdQueryHandler(ILogger<GetAdminByIdQueryHandler> logger,
    IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<GetAdminByIdQuery, AdminDto>
{
    public async Task<AdminDto> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting admin with id: {adminId}",request.Id);
        var admin = await adminsRepository.GetById(request.Id)
             ?? throw new NotFoundException(nameof(User), request.Id.ToString());
        var adminDto = mapper.Map<AdminDto>(admin);
        return adminDto;
    }
}
