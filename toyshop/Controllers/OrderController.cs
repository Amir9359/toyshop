using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models;
using toyshop.Models.Payment;
using toyshop.Models.Products;
using toyshop.Models.Profile;
using toysite.Models;

namespace toyshop.Controllers
{
    public class OrderController : Controller
    {
        private IPayRepository _payRepo;
        private ApplicationDbContext _context;
        private UserManager<User> _UserManager;
        private IAddressRepository _AddressRepo;
        private IPayItemRepository _payItemRepository;
        private IProductItemRepository _ProductItemRepo;

        public OrderController(IProductItemRepository ProductItemRepo, IPayRepository payRepo,
            IPayItemRepository payItemRepository, UserManager<User> UserManager, IAddressRepository AddressRepo, ApplicationDbContext context)
        {
            _UserManager = UserManager;
            _payRepo = payRepo;
            _AddressRepo = AddressRepo;
            _context = context;
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
        public async Task<IActionResult> Save(string Fish)
        {
            //------------cart-----------
            var customer = await _UserManager.FindByNameAsync(User.Identity.Name);
            var Pay = await _payRepo.Find(customer.Id);
            var TotalPrice = Pay.PayItems.Sum(p => p.Product.Price * p.Quantity);
            //-----------------------addOrder-----------------------
            var Payed = new PayedCart();
            Payed = new PayedCart
            {
                Date = DateTime.Now,
                FishSabt = Fish,
                Cusstomer = customer
            };
            await _context.PayedCarts.AddAsync(Payed);
            await _context.SaveChangesAsync();

            Payed.PayedCartItemID = Payed.Id;

            var payedItem = new List<PayedCartItems>();
            foreach (var payItem in Pay.PayItems)
            {
                payedItem.Add(new PayedCartItems
                {
                    ProductId = payItem.Product.Id,
                    PayedCartid = Payed.Id
                }
                );
            }
            await _context.PayedCartItems.AddRangeAsync(payedItem);
            await _context.SaveChangesAsync();

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

        public async Task<IActionResult> DeleteCartItem(int id)
        {
            ////اپدیت موجودی کالا/////
            var cartItem = await _payItemRepository.FindItem(id);
            var Product = await _context.Products.FindAsync(cartItem.Product.Id);
            Product.Count = Product.Count + cartItem.Quantity;
            await _context.SaveChangesAsync();


            var res = await _payItemRepository.DeleteCartItem(id);
            if (res)
            {
           
                await _payItemRepository.save();
                return RedirectToAction("Index", "Order");
            }
            return RedirectToAction("Index", "Order");
        }

    }
}
