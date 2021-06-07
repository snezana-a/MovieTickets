using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.Models
{
    public class Movie : BaseEntity
    {
        public string MovieName { get; set; }
        public string MovieImage { get; set; }
        public string Genre { get; set; }
        public int TicketPrice { get; set; }
        public DateTime Showtime { get; set; }
        public virtual ICollection<TicketsInCart> CartTickets { get; set; }
        public IEnumerable<OrderedTickets> OrderedTickets { get; set; }
    }
}
