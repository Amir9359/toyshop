using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Products;

namespace toyshop.Models.Products
{
   public  interface IProductRepository
    {
        Task AddAsync(Product Product);
        Task DeleteAsync(int Id);
        Task<Product> FindAsync(int id);
        Task<Product> ProductDetailAsync(int Productid);
        Task<IEnumerable<Product>> SearchAsync(int? id , string PrimaryTitle);
        void Update(Product product);
        Task<IEnumerable<Product>> SearchAsync(int Groupid);
        Task saveAsync();
    }
}
