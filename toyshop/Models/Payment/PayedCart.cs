using System;
using System.Collections.Generic;
using toyshop.Models.Products;

namespace toyshop.Models.Payment
{
    public class PayedCart
    {
        public int Id { get; set; }
        public string FishSabt { get; set; }
        public DateTime Date { get; set; }
        public ICollection<PayedCartItems> PayedCarts { get; set; }
        public int PayedCartItemID { get; set; }
        public User Cusstomer { get; set; }
    }
}
