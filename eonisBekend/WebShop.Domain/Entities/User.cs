using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string Salt { get; set; }

        public Address? Address { get; set; }
        public string? ContactNumber { get; set; }

        public List<Order>? Orders { get; set; }

        



    }
}
