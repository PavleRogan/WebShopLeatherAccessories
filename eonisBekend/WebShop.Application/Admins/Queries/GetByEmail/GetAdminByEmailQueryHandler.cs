using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Admins.Dtos;
using WebShop.Application.Users.Queries.GetByEmail;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Admins.Queries.GetByEmail;

public class GetAdminByEmailQueryHandler(ILogger<GetAdminByEmailQueryHandler> logger,
    IAdminsRepository adminsRepository,
    IMapper mapper) : IRequestHandler<GetAdminByEmailQuery, AdminDto>
{
    public async Task<AdminDto> Handle(GetAdminByEmailQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting admin by email:{email}", request.Email);
        var admin = await adminsRepository.GetByEmail(request.Email)
            ?? throw new NotFoundException(nameof(Admin), request.Email.ToString());
        var userDto = mapper.Map<AdminDto>(admin);
        return userDto;
    }
}
