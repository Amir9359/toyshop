using System;
using System.Collections.Generic;
using toyshop.Models.Groups;
using toysite.Models;

namespace toyshop.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondTitle { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Group group { get; set; }
        public int Count { get; set; }
        public int groupid { get; set; }
        public State state { get; set; }

        public User Creator { get; set; }
        public DateTime CreateDate { get; set; }

        public List<ProductItem> ProductItems { get; set; }
    }
}