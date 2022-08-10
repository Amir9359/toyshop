using Microsoft.EntityFrameworkCore;
using toyshop.Models;
using toyshop.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toysite.Models;

namespace toyshop.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public async Task<Address> Find(string customerId)
        {
            var Address = await _context.Addresses.SingleOrDefaultAsync(c => c.Customer.Id == customerId);
            return Address;
        }

        public async Task<bool> EditeProfile(Address newaddres , string CustomerId)
        {
            var address =await _context.Addresses.SingleOrDefaultAsync(s => s.Customer.Id == CustomerId) ;

            if (address != null)
            {

                address.Location = newaddres.Location;
                address.Ostan = newaddres.Ostan;
                address.Phone = newaddres.Phone;
                address.Shahr = newaddres.Shahr;
 
                _context.Addresses.Update(address) ;
                return true;    
            }

            return false;
        }

        public async Task save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
