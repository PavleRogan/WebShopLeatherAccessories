using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Admins.Dtos;
using WebShop.Domain.Repositories;

namespace WebShop.Application.Admins.Queries.GetAll;

public class GetAllAdminsQueryHandler(ILogger<GetAllAdminsQueryHandler> logger,
    IMapper mapper, IAdminsRepository adminsRepository) : IRequestHandler<GetAllAdminsQuery, IEnumerable<AdminDto>>
{
    public async Task<IEnumerable<AdminDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all admins");
        var admins = await adminsRepository.GetAllAsync();
        var adminsDtos =  mapper.Map<IEnumerable<AdminDto>>(admins);
        return adminsDtos;
    }
}
