using Microsoft.AspNetCore.Mvc;
using toyshop.Models.Products;
using toyshop.Models.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.InfraStructure;
using toyshop.InfraStructure;
using toyshop.Models.Products;

namespace toyshop.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _ProductRepository;
        public ProductController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            var product = await _ProductRepository.ProductDetailAsync(id);
            return View(product);
        }
        public async Task<IActionResult> List()
        {


            return View();
        }

    }
}