using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models;
using toysite.Models;

namespace toyshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : BaseController
    {
        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;
        public HomeController(UserManager<User> UserManager, SignInManager<User> SignInManager) : base(UserManager)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
        }
        // GET: HomeController

        public async Task<IActionResult> Index()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var claim = await UserManager.GetClaimsAsync(user);
            if (claim.Any(t => t.Value != "Operator"))
            {
              await  SignInManager.SignOutAsync();
              return RedirectToAction("Signin","Account");
            }
            return View();
        }


    }
}
