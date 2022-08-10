using System;
using System.Collections.Generic;
using toysite.Models;

namespace toyshop.Models.Payment
{
    public class Pay
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public User Customer { get; set; }
        public double TotalPrice { get; set; }

        public PayState PayState { get; set; }
        public DateTime? PayDate { get; set; }
        public List<PayItems> PayItems { get; set; }

    }
}