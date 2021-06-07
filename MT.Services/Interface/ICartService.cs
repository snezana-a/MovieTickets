using MT.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Services.Interface
{
    public interface ICartService
    {
        CartDto getCartInfo(string userId);
        bool deleteTicket(string userId, Guid id);
        bool orderNow(string userId);
    }
}
