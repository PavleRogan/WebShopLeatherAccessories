using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Entities
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //dodaj posle enkripciju
    }
}
