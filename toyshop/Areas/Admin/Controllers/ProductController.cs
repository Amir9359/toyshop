using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toyshop.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Digikala.Areas.Admin;
using Project_Digikala.Areas.Admin.Controllers;
using toyshop.Models;
using toyshop.Models.Products;
using toyshop.Models.Groups;
using toyshop.Models.ViewModels.Product;
using toyshop.Models.Payment;
using toyshop.Models.Profile;
using toysite.Models;


namespace toyshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : BaseController
    {
        #region Properties
        private UserManager<User> UserManager;
        private IProductRepository _productRepo;
        private IGroupRepository groupRepo;
        private IHostingEnvironment hosting;
        private IProductItemRepository ProductItemRepo;

        public ProductController(UserManager<User> userManager, IProductRepository productRepo, 
            IGroupRepository groupRepo, IHostingEnvironment hosting, IProductItemRepository productItemRepo) :base(userManager)
        {
            UserManager = userManager;
            _productRepo = productRepo;
            this.groupRepo = groupRepo;
            this.hosting = hosting;
            ProductItemRepo = productItemRepo;
        }

        #endregion /Properties

        #region Constractor 
 
        #endregion /Constractor

        #region HttpGet 
        public IActionResult Index(int Id)
        {
            return View();
        }


        public async Task<IActionResult> list(int? id, string PrimaryTitle)
        {
            var breadcrumbs = new List<breadcrumb>()
            {
                new breadcrumb
                {
                    Title="صفحه اصلی ",
                    Url ="/"
                } ,
                new breadcrumb
                {
                    Title="لیست کالا ها ",
                    Url =""
                }
            };

            var products = await _productRepo.SearchAsync(id, PrimaryTitle);
            var productList = new List<ProductView>();
            var persian = new PersianCalendar();
            if (products != null)
            {
                foreach (var item in products)
                {
                    productList.Add(new ProductView
                    {
                        Id = item.Id,
                        PrimaryTitle = item.PrimaryTitle,
                        SecondTitle = item.SecondTitle,
                        Description = item.Description,
                        group = new Group
                        {
                            Id = item.group.Id,
                            Title = item.group.Title
                        },
                        state = item.state,
                        Creator = item.Creator?.Name + " " + item.Creator?.LastName,
                        CreateDate = persian.PersianDate(item.CreateDate),
                    });
                }

            }
            return View(productList);
        }


        public async Task<IActionResult> Add()
        {
     
            return View();
        }


        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.Idd = 1;

            //بایند گروه و برند کالا
            var GroupbrandModel = new GoupBrandView();
 
            var product = await _productRepo.FindAsync(Id);
            GroupbrandModel.product = new Product
            {
                Id = Id,
                groupid = product.groupid,
                PrimaryTitle = product.PrimaryTitle,
                SecondTitle = product.SecondTitle,
                state = product.state,
                Description = product.Description,
                Price = product.Price
            };
            var selectgroup = await groupRepo.FindAsync(GroupbrandModel.product.groupid);

            GroupbrandModel.Groups = new SelectList(selectgroup, "Id", "Title");

            return View("Add", GroupbrandModel);
        }


        public async Task<IActionResult> Delete(int Id)
        {
            var productitem = new ProductItem
            {
                Product = new Product
                {
                    Id = Id
                }
            };
 

            var Pitem = await ProductItemRepo.search(productitem.Id);
            if (Pitem != null && Pitem.Count != 0)
            {
                await ProductItemRepo.Delete(Pitem.SingleOrDefault().Id);
                await ProductItemRepo.save();
            }


            await _productRepo.DeleteAsync(Id);
            await _productRepo.saveAsync();
            return RedirectToAction("List");
        }
 
        #endregion /HttpGet

        #region HttpPost
        [HttpPost]
        public async Task<IActionResult> save(int? Id, string PrimaryTitle, string SecondaryTitle, string Description,
            int brand, int Group, int Price , byte state, IFormFile Photo)
        {
            if (Id == null)
            {
                Product product = new Product
                {
                    groupid = Group,
                    PrimaryTitle = PrimaryTitle,
                    SecondTitle = SecondaryTitle,
                    state = (State)state,
                    Description = Description,
                    Creator = new User()
                    {
                        Id = this.Operator.Id
                    },
                    CreateDate = DateTime.Now,
                    Price = Price
                };

                await _productRepo.AddAsync(product);
                await _productRepo.saveAsync();
                var productid = product.Id;

                var ext = Path.GetExtension(Photo.FileName);
                var path = Path.Combine(hosting.WebRootPath + "\\media\\blog-preview", productid + " "+ ext);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await Photo.CopyToAsync(filestream);
                }
                return RedirectToAction("List");

            }
            else
            {//edit
                var op = await UserManager.FindByIdAsync(this.Operator.Id);
                Product product = new Product
                {
                    Id = (int)Id,
                    groupid = Group,
                    PrimaryTitle = PrimaryTitle,
                    SecondTitle = SecondaryTitle,
                    state = (State)state,
                    Description = Description,
                    Price = Price
                };
                _productRepo.Update(product);
                await _productRepo.saveAsync();

                return RedirectToAction("List");
            }
        }


  
        #endregion /HttpPost
    }
}
