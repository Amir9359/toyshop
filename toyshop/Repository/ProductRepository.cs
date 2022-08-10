using Microsoft.EntityFrameworkCore;
using toyshop.Models;
using toyshop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.InfraStructure;
using toysite.Models;

namespace toyshop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Product product)
        {
            await context.AddAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            Product p = await context.Products.FindAsync(id);
            context.Remove(p);
        }

        public async Task<Product> FindAsync(int id)
        {
            return await context.Products.Include(b => b.group).Where(p => p.Id == id).ToAsyncEnumerable().SingleOrDefault();
        }


        public async Task<Product> ProductDetailAsync(int Productid)
        {
            return await context.Products
                .Include(g => g.group)
                .Include(item => item.ProductItems)
                .Where(p => p.Id == Productid)
                .FirstOrDefaultAsync()
                ;
        }

        public async Task saveAsync()
        {
            await context.SaveChangesAsync();
        }
        public void Update(Product product)
        {//اگر تنها اپدیت کنیم مقدار زمان ایجاد 000 0000 میشود  پس باید مقادیری که میخواهیم اپدیت شود تغییر دهیم
            context.Products.Update(product);
            context.Entry(product).Property(p => p.CreateDate).IsModified = false;
            context.Entry(product).Reference(p => p.Creator).IsModified = false;
        }
        public async Task<IEnumerable<Product>> SearchAsync(int? id, string PrimaryTitle)
        {
            var query = await context.Products 
                .Include(b => b.Creator).Include(g => g.group)
                .Where(p => (p.Id == id || id == null) && (p.PrimaryTitle == PrimaryTitle || PrimaryTitle.CheckStringIsnull()))
                .ToAsyncEnumerable().ToList();
            return query;
        }

 
        public async Task<IEnumerable<Product>> SearchAsync(int Groupid)
        {
            var query = await context.Products
                .Include(g => g.group).Include(item => item.ProductItems)
            .Where(p => (p.groupid == Groupid)).ToAsyncEnumerable().ToList();
            return query;
        }

 
    }
}
