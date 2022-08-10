using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Products;

namespace toyshop.Models.Payment
{
    public class PayItems
    {
        public long Id { get; set; }
        public Pay pay { get; set; }
        public Product Product{ get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
