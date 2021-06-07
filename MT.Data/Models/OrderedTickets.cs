using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.Models
{
    public class OrderedTickets : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Movie SelectedMovie { get; set; }
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }
    }
}
