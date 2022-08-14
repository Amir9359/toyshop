using System.Collections.Generic;
using toyshop.Models.Products;

namespace toyshop.Models.Payment
{
    public class PayedCartItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PayedCartid { get; set; }
        public PayedCart PayedCarts { get; set; }
    }
}