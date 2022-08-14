using toyshop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Groups;

namespace toyshop.Models.ViewModels.Product
{
    public class ProductView
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondTitle { get; set; }
        public string Description { get; set; }
        public Group group { get; set; }
 
        public State state { get; set; }
        public int Count { get; set; }

        public string Creator { get; set; }
        public string CreateDate { get; set; }

    }
}
