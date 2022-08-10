 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toysite.Models;

namespace toyshop.Models.Products
{
    public class ProductItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public byte Quantity { get; set; }
        public double? Takhfif { get; set; }

        public State state { get; set; }
        public User Creator { get; set; }
        public DateTime CreateDate { get; set; }
 
    }
}
