using toyshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Groups;

namespace toyshop.Models.ViewModels.Product
{
    public class ProductListView
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public Group Group { get; set; }
    }
}
