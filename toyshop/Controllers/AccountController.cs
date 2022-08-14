using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toyshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.InfraStructure;
using toysite.Models;

namespace toyshop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> SignInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = SignInManager;
        }

        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signin(string email, string password, bool rememberme)
        {
            if (!email.CheckStringIsnull() && !password.CheckStringIsnull())
            {
                var user = await UserManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var signin = await SignInManager.PasswordSignInAsync(user, password, rememberme, false);
                    if (signin.Succeeded)
                    {
                        var claimCheck = await UserManager.GetClaimsAsync(user);
                        if (claimCheck.Any(t=>t.Value != "Customer") || claimCheck.Count == 0)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.Error = "نام کاربری و  رمز عبور را  صحیح وارد کنید !";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "نام کاربری و  رمز عبور را  صحیح وارد کنید !";
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "نام کاربری یا  رمز عبور را  وارد کنید !";
                return View();
            }
            ViewBag.Error = "نام کاربری یا  رمز عبور را  صحیح وارد کنید !";
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(string firstName, string lastName ,string email, string password)
        {
            if (!email.CheckStringIsnull()  && !firstName.CheckStringIsnull() && !lastName.CheckStringIsnull())
            {
                var customer = new User
                {
                    UserName = email,
                    Email = email,
                    Name = firstName,
                    LastName = lastName
                };
                var signup = await UserManager.CreateAsync(customer, password);

                if (signup.Succeeded)
                {
                    await Signin(email, password, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "ثبت نام با خطا مواجه شد !";
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "لطفا مقادیر را کامل پر کنید.";
                return View();

            }
        }

        public async Task<IActionResult> Signout()
        {
            await SignInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
