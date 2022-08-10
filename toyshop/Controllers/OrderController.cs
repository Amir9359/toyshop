using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toyshop.Models;
using toyshop.Models.Products;
using toyshop.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.InfraStructure;
using toyshop.Models.Payment;
using toysite.Models;

namespace toyshop.Controllers
{
    public class OrderController : Controller
    {
        private IPayRepository _payRepo;
        private UserManager<User> _UserManager;
        private IAddressRepository _AddressRepo;
        private IPayItemRepository _payItemRepository;
        private IProductItemRepository _ProductItemRepo;
        public OrderController(IProductItemRepository ProductItemRepo, IPayRepository payRepo,
            IPayItemRepository payItemRepository, UserManager<User> UserManager, IAddressRepository AddressRepo)
        {
            _UserManager = UserManager;
            _payRepo = payRepo;
            _AddressRepo = AddressRepo;
            _payItemRepository = payItemRepository;
            _ProductItemRepo = ProductItemRepo;
        }
        public async Task<IActionResult> Index()
        {
            //-------cart-----------
            var customer = await _UserManager.FindByNameAsync(User.Identity.Name);
            var cart = await _payRepo.Find(customer.Id);
            ViewBag.TotalPrice = "0";
            if (cart != null)
            {
                ViewBag.TotalPrice = cart.PayItems.Sum(p => p.Product.Price * p.Quantity).ToString("N0");
            }

            //------------------Address--------------
            var Address = await _AddressRepo.Find(customer.Id);
            if (Address != null)
            {
                return View(Address);
            }
            else
            {
                return RedirectToAction("Index", "Profile");
            }
        }
        public async Task<IActionResult> Save()
        {
            //------------cart-----------
            var customer = await _UserManager.FindByNameAsync(User.Identity.Name);
            var Pay = await _payRepo.Find(customer.Id);
            var TotalPrice = Pay.PayItems.Sum(p => p.Product.Price * p.Quantity);
            //-----------------------addOrder-----------------------
            var Payitem = new Pay
            {
                Customer = customer,
                PayDate = DateTime.Now,
                TotalPrice = TotalPrice,
                PayState = PayState.UnPaied
            };

            var OrderItems = new List<PayItems>();
            foreach (var cartItems in Pay.PayItems)
            {
                OrderItems.Add(new PayItems
                {
                    pay = Payitem,
                    Price = cartItems.Product.Price,
                    Product = cartItems.Product,
                    Quantity = cartItems.Quantity

                });

            }

            _payRepo.DeletPayItems(Pay.PayItems);
            await _payRepo.save();

            await _payRepo.DeletePay(Pay.Id);
            await _payRepo.save();

            return RedirectToAction("Index", "Home");
        }
 
 
 
    }
}
