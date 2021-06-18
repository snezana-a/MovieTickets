using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MT.Services.Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTickets.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._cartService.getCartInfo(userId));
        }
        public IActionResult DeleteTicket(Guid ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._cartService.deleteTicket(userId, ticketId);
            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }
        
        public Boolean OrderTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._cartService.orderNow(userId);

            return result;
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this._cartService.getCartInfo(userId);
            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });
            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "MovieTickets Payment",
                Currency = "usd",
                Customer = customer.Id
            });
            if (charge.Status == "succeeded")
            {
                var result = this.OrderTickets();
                if (result)
                {
                    return RedirectToAction("Index", "Cart");
                }
                else
                {
                    return RedirectToAction("Index", "Cart");
                }
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
