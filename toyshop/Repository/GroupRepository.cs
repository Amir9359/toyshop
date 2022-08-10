using Microsoft.EntityFrameworkCore;
using toyshop.Models;
using toyshop.Models.Products;
using toyshop.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toyshop.Models.Groups;
using toysite.Models;

namespace toyshop.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private ApplicationDbContext context;
        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Group group)
        {
           await context.Groups.AddAsync(group);
        }

        public async Task DeleteAsync(int id)
        {
            Group g =  await context.Groups.FindAsync(id);
            context.Groups.Remove(g);
        }

        public async Task<List<Group>> FindAsync(int id) 
        {
            var f= await context.Groups.Include(o=>o.Creator).Where(g=>g.Id==id).ToListAsync();
            return f;
        }

        public async Task saveAsync()
        {
          await  context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> SearchAsync(string title, int? id, State? state)
        {
            var Query = await context.Groups.Include(o => o.Creator)
                .ToAsyncEnumerable().ToList();
            var Groups = await Query.Where(b => (b.Title == title || string.IsNullOrEmpty(title)) 
                                                && (b.Id == id || id == null)).ToAsyncEnumerable().ToList();

            return Groups;
        }
        public async Task Update(Group group)
        {
            var grp= await context.Groups.FindAsync(group.Id);
            grp.Id = group.Id;
            grp.Title = group.Title;
            grp.SecondTitle = group.SecondTitle;

        }
    }
}
