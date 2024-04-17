using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.Application.Users.Dtos
{
    public class CreateUserDto
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string? City { get; set; }
        public string? StreetAndNumber { get; set; }
        public string? PostalCode { get; set; }

        public string? ContactNumber { get; set; }

    }
}
