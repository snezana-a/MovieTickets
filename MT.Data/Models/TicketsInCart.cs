using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.Models
{
    public class TicketsInCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Movie Movie { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
    }
}
