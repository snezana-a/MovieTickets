using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MT.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTickets.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
       
        
        public IActionResult AddToRole(string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            
            //var roles = manage.UserRole;

            return RedirectToAction("Index");
        }
    }
}
