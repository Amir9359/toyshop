using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Products;
using toyshop.Models.ViewModels.Product;

namespace toyshop.Models.ViewModels
{
    public class ProductItemView
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public byte Quantity { get; set; }
        public  ProductView Product { get; set; }

        public State state { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
 
    }
}
