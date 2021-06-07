using Microsoft.EntityFrameworkCore;
using MT.Data.Models;
using MT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.OrderedTickets)
                .Include(z => z.User)
                .Include("OrderedTickets.SelectedMovie")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Include(z => z.OrderedTickets)
                .Include(z => z.User)
                .Include("OrderedTickets.SelectedMovie")
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
