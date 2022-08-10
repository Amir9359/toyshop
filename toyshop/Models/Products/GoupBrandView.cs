using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Products;
using toyshop.Migrations;

namespace toyshop.Models.Products
{
    public class GoupBrandView
    {
        public SelectList Brands { get; set; }
        public SelectList Groups { get; set; }
        public  Products.Product product { get; set; }
    }
}
