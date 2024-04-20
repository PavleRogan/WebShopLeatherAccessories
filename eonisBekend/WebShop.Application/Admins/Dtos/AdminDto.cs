using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Admins.Dtos;

public class AdminDto
{
    public Guid AdminId { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
