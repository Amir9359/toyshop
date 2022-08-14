using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toyshop.Models.Payment
{
   public interface IPayItemRepository
    {
        Task Add(Pay order);
        Task Add(List<PayItems> PayItems);
        Task<Pay> Find(int orderId);
        Task<PayItems> FindItem(int id);

        Task Update(int payid, string FishNumber , string PayDate);
        Task<List<PayItems>> search(string customerId);
        Task<bool> DeleteCartItem(int id);
        Task save();
    }
}
