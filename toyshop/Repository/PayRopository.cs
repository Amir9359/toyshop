using Microsoft.EntityFrameworkCore;
using toyshop.Models;
using toyshop.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Payment;
using toysite.Models;

namespace toyshop.Repository
{
    public class PayRopository : IPayRepository
    {
        private ApplicationDbContext _Context;
        public PayRopository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task Add(Pay Pay)
        {
            await _Context.Pays.AddAsync(Pay);
        }

        public async Task Add(PayItems PayItem)
        {
            await _Context.PayItems.AddAsync(PayItem);
        }


        public void DeletPayItems(List<PayItems> PayItems)
        {
            _Context.PayItems.RemoveRange(PayItems);
        }

 

        public async Task Delete(int cartItemId)
        {
            var cartItem = await _Context.PayItems.FindAsync(cartItemId);
            _Context.PayItems.Remove(cartItem);
        }

 
        public async Task DeletePay(int payId)
        {
            var Cart=  await _Context.Pays.FindAsync(payId);
            _Context.Pays.Remove(Cart);
        }

 
        public async Task<Pay> Find(int payid)
        {
            var pay = await _Context.Pays.FindAsync(payid);
            return pay;
        }
        public async Task<Pay> Find(string customerid)
        {
            var cart = await _Context.Pays.Include(c => c.Customer)
                .Include(c => c.PayItems)
                .ThenInclude(i => i.Product)
                .SingleOrDefaultAsync(c => c.Customer.Id == customerid);
            return cart;
        }


        public async Task save()
        {
            await _Context.SaveChangesAsync();
        }

        public async Task<List<Pay>> search(string customeerId)
        {
            var carts = await _Context.Pays.Where(c => c.Customer.Id == customeerId)
               .Include(c => c.Customer).ToAsyncEnumerable().ToList();
            return carts;
        }

        public async Task Update(PayItems item)
        {
            var cartItem = await _Context.PayItems.FindAsync(item.Id);
            var Quantity = item.Quantity + cartItem.Quantity;
            cartItem.Id = item.Id;
            cartItem.Quantity = Quantity;

            _Context.PayItems.Update(cartItem);
            _Context.Entry(cartItem).Reference(c => c.pay).IsModified = false;
            _Context.Entry(cartItem).Reference(c => c.Product).IsModified = false;

        }

 
    }
}
