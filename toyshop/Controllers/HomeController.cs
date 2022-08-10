using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using toyshop.Models;
using toyshop.Models.Products;
using toysite.Models;

namespace toyshop.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;
        private ApplicationDbContext _context;
        public HomeController(UserManager<User> UserManager, SignInManager<User> SignInManager, ApplicationDbContext context)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            _context = context;
        }
        public async Task<IActionResult> Index(int? groupid)
        {
            IEnumerable<Product> products = null;
            if (User.Identity.Name != null)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                if ( user.UserName == "digiAdmin")
                {
                    await SignInManager.SignOutAsync();
                    return Redirect("/");
                }
            }

            if (groupid == null)
            { 
                products = await _context.Products.Include(c => c.group).ToListAsync();
            }
            else
            {
                products = await _context.Products.Include(c => c.group)
                    .Where(c => c.group.Id == groupid).ToListAsync();

            }
            return View(products);
        }
        public async  Task<IActionResult> List()
        {
             var products = await _context.Products.Include(c => c.group).ToListAsync();

            return View(products);
        }
        public IActionResult ContacttUs()
        {
            return View();
        }

        public async Task<IActionResult> Search(string Search)
        {
            var Toys = await _context.Products.Include(g => g.group)
                .Where(s => s.PrimaryTitle.Contains(Search)  || s.SecondTitle.Contains(Search)).ToListAsync();
            if (Search ==null)
            {
                Toys = await _context.Products.Include(g => g.group).ToListAsync();
            }
            return View(Toys);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var Product = await _context.Products.Include(g => g.group)
                .Where(d => d.Id == Id).FirstOrDefaultAsync();

            if (Product != null)
            {
                return View(Product);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
