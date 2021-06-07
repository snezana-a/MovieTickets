using MT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data.DTO
{
    public class CartDto
    {
        public List<TicketsInCart> TicketsInCart { get; set; }
        public double TotalPrice { get; set; }
    }
}
