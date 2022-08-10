using Microsoft.EntityFrameworkCore;
using toyshop.InfraStructure;
using toyshop.Models;
using toyshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Payment;
using toysite.Models;

namespace toyshop.Repository
{
    public class PayItemRepository : IPayItemRepository
    {
        private ApplicationDbContext _Context;
        public PayItemRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task Add(Pay order)
        {
            await _Context.Pays.AddAsync(order);
        }

        public async Task Add(List<PayItems> orderItems)
        {
            await _Context.PayItems.AddRangeAsync(orderItems);
        }

        public async Task<Pay> Find(int orderId)
        {
            var pay = await _Context.Pays
               .Include(o => o.PayItems)
               .ThenInclude(p => p.Product)
               .ThenInclude(p => p.ProductItems)
               .Include(c => c.Customer)
               .FirstOrDefaultAsync(o => o.Id == orderId);
            return pay;
        }

        public async Task save()
        {
            await _Context.SaveChangesAsync();
        }

        public async Task  Update(int orderid, string FishNumber, string PayDate)
        {
            var ord =await _Context.Pays.FindAsync(orderid);
            ord.PayState = PayState.Paied;
            ord.PayDate = PayDate.ToMiladiDate();

            _Context.Entry(ord).Reference(c => c.Customer).IsModified = false;

        }
    }
}
