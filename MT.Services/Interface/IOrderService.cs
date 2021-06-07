using MT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Services.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
