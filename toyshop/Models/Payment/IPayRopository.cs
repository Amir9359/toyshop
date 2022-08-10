using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toyshop.Models.Payment
{
    public interface IPayRepository
    {
        Task Add(Pay pay);
        Task Add(PayItems payItem);
        Task<Pay> Find(int payItemid);
        Task<Pay> Find(string customerid);
        Task<List<Pay>> search(string customerId);
        Task Update(PayItems item);
        Task Delete(int payItemId);
 
        Task DeletePay(int payId);
        void  DeletPayItems(List<PayItems> payItems);
 
        Task save();
    }
}
