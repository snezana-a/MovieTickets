using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MT.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Admin")).ToList();
            var users = (await _userManager.GetUsersInRoleAsync("Customer")).ToList();

            var model = new ManageRoles
            {
                Admins = admins,
                Users = users
            };

            return View(model);
        }
       /* public IActionResult AddToRole(Guid? id)
        {
            ManageRoles roles = new ManageRoles();
            var role = roles.
        }*/
    }
}
