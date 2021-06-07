using MT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
