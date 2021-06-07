using Microsoft.AspNetCore.Mvc;
using MT.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTickets.Controllers
{
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
    }
}
