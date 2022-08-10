using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toyshop.Models.Products
{
    public interface IProductItemRepository
    {
        Task Add(ProductItem productItem);
        Task Update(ProductItem productItem);
        Task Update(List<ProductItem>  productItem);
        Task Delete(int? id);
        Task<List<ProductItem>> search(int Productid);
        Task<ProductItem> Find(int id);
        Task save();
    }
}
