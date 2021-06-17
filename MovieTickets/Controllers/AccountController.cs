using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MT.Data.Identity;
using MT.Data.Models;
using MT.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await userManager.GetUsersInRoleAsync("Admin")).ToList();
            var users = (await userManager.GetUsersInRoleAsync("Customer")).ToList();
            
            var model = new ManageRoles
            {
                Admins = admins,
                Users = users
            };

            return View(model);
        }
        public IActionResult Register()
        {
            UserRegisterDto model = new UserRegisterDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new AppUser
                    {
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserCart = new Cart()
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        if(!await roleManager.RoleExistsAsync(UserRole.AdminUser))
                        {
                            await roleManager.CreateAsync(new IdentityRole(UserRole.AdminUser));
                            await userManager.AddToRoleAsync(user, UserRole.AdminUser);
                        }
                        if (!await roleManager.RoleExistsAsync(UserRole.Customer))
                        {
                            await roleManager.CreateAsync(new IdentityRole(UserRole.Customer));
                            await userManager.AddToRoleAsync(user, UserRole.Customer);
                        }
                        await userManager.AddToRoleAsync(user, UserRole.Customer);

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> AddToRoleAsync(AppUser user, string role)
        {
            var oldRole = "";
            if (await userManager.IsInRoleAsync(user, "Customer"))
            {
                oldRole = "Customer";
            }
            else
            {
                oldRole = "Admin";
            }

            await userManager.RemoveFromRoleAsync(user, oldRole);

            await userManager.AddToRoleAsync(user, role);

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
