using MT.Data.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.Models
{
    public class Cart : BaseEntity
    {
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public virtual ICollection<TicketsInCart> CartTickets { get; set; }
    }
}
