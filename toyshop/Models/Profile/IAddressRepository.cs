using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toyshop.Models.Profile
{
   public  interface IAddressRepository
    {
        Task Add(Address address);
        Task<Address> Find(string CustomerId);
        Task save();
        Task<bool> EditeProfile(Address newaddres, string CustomerId);
    }
}
