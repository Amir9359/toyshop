using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Groups;
using toyshop.Models.Products;

namespace toyshop.Models.Groups
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group);
        Task Update(Group group);
        Task DeleteAsync(int id);
        Task<List<Group>> FindAsync(int id);
        Task saveAsync();
    }
}
