using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toyshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toysite.Models;

namespace toyshop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AccountController : Controller
    {

        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;
        public AccountController(UserManager<User> userManager , SignInManager<User> SignInManager)  
        {
            this.UserManager = userManager;
            this.SignInManager = SignInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Signout()
        {
            await SignInManager.SignOutAsync();
            return Redirect("/");
        }
        public IActionResult Signin(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signin(string username, string password, bool rememberme )
        {
      
        var user= await UserManager.FindByNameAsync(username);
            if (user != null)
            {
                var claim = await UserManager.GetClaimsAsync(user);
                if (claim.Any(t => t.Value == "Operator"))
                {
                    var result = await SignInManager.PasswordSignInAsync(user, password, rememberme, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.error = " نام کاربری و رمز عبور اشتباه است";
                        return View();
                    }
                }
            }
            ViewBag.error = " نام کاربری و رمز عبور اشتباه است";
            return View();
        }
    }
}
