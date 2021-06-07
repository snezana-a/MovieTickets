using Microsoft.AspNetCore.Identity;
using MT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public virtual Cart UserCart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
