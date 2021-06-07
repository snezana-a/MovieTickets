using MT.Data.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public virtual ICollection<OrderedTickets> OrderedTickets { get; set; }
    }
}
