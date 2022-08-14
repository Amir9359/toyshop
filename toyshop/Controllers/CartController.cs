using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toyshop.Models;
using toyshop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Payment;
using toyshop.Models.Payment;
using toysite.Models;

namespace toyshop.Controllers
{
    public class CartController : Controller
{
        private UserManager<User> _UserManager;
        private IPayRepository _payRopo;
        private IProductRepository _ProductRepo;
        public CartController(UserManager<User> UserManager, IPayRepository payRopo, IProductRepository ProductRepo)
        {
            _UserManager = UserManager;
            _payRopo = payRopo;
            _ProductRepo = ProductRepo;
        }
    public async Task<IActionResult> Index(int? productitemsid, int count = 1)
    {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Signin", "Account");
            }
            else 
            {
                var user = await _UserManager.FindByNameAsync(User.Identity.Name);
                var pay = await _payRopo.Find(user.Id);
                if (productitemsid != null && count != null)
                {
                    var Pitem = await _ProductRepo.FindAsync((int)productitemsid);
                    if (pay != null && Pitem.Count >= count)
                    {
                        if (pay.PayItems.Any(c=>c.Product.Id == productitemsid))
                        {
                            var payItem = pay.PayItems.FirstOrDefault(c => c.Product.Id == productitemsid);
                            await _payRopo.Update(new PayItems
                            {
                                Id=payItem.Id,
                                Quantity=(int)count,
                                Price = Pitem.Price
                            });
                            Pitem.Count = Pitem.Count - count;
                            await _payRopo.save();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            await _payRopo.Add(new PayItems
                            {
                                pay = pay,
                                Product  = Pitem,
                                Quantity = (int)count,
                                Price = Pitem.Price
                            });
                            Pitem.Count = Pitem.Count - count;
                            await _payRopo.save();

                            return RedirectToAction("Index" , "Home");
                        }

                    }
                    else
                    {
                        var newpay = new Pay
                        {
                            Customer = user,
                            Date = DateTime.Now,
                            PayState = PayState.UnPaied,
                            
                        };
                        await _payRopo.Add(newpay);
                        await _payRopo.save();

                        var payfinal = await _payRopo.Find(newpay.Id);
                        await _payRopo.Add(new PayItems
                        {
                            pay = payfinal,
                            Product = Pitem,
                            Quantity = (int)count,
                            Price = Pitem.Price
                        });
                        Pitem.Count = Pitem.Count - count;

                        await _payRopo.save();

                        var sCart = await _payRopo.Find(user.Id);
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                else 
                {
                   
                    return RedirectToAction("Index", "Home");
                }
            }
        
    }
    public async Task<IActionResult> Remove(int id)
    {
           await _payRopo.Delete(id);
           await _payRopo.DeletePay(id);
            await _payRopo.save();
            return new RedirectResult("/Cart");
    }
}
}
