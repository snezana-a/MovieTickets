using MT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.DTO
{
    public class AddToCartDto
    {
        public Movie SelectedTicket { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
