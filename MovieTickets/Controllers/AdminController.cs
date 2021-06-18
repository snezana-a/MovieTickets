using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MT.Data.Identity;
using MT.Data.Models;
using MT.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            this._orderService = orderService;
            this._userManager = userManager;
        }

        public List<Order> GetOrders()
        {
            return this._orderService.GetAllOrders();
        }

        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this._orderService.GetOrderDetails(model);
        }

        public bool ImportUsers(List<UserRegisterDto> model)
        {
            bool status = true;
            foreach (var item in model)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var user = new AppUser
                    {
                        UserName = item.Email,
                        NormalizedEmail = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserCart = new Cart()
                    };

                    var result = _userManager.CreateAsync(user, item.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
